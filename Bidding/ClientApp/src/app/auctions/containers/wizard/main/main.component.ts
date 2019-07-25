// angular
import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

// 3rd party
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';
import { startWith } from 'rxjs/operators';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker/bs-datepicker.config';
import { MovingDirection } from 'angular-archwizard';

// internal
import { AuctionsService } from '../../../services/auctions.service';
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';
import { AuctionAddCategoryWizardStepComponent } from '../../../components/wizard/wizard-steps/category-step/category-step.component';
import { CategoryConstants } from 'ClientApp/src/app/core/constants/categories/category-constants';
import { CategoriesWithTypesModel } from '../../../models/add/categories-with-types.model';
import { TypeModel } from '../../../models/shared/types/type.model';
import { AuctionAddAboutWizardStepComponent } from '../../../components/wizard/wizard-steps/about-step/about-step.component';


@Component({
  templateUrl: './main.component.html'
})
export class AuctionAddMainWizardComponent implements OnInit, AfterViewInit {
  auctionAddSubscription: Subscription;
  submitted = false;

  // filters
  categories: CategoriesWithTypesModel;
  auctionTypes: TypeModel[];

  // API
  addAuctionRequest: Auctions.AddAuctionRequestModel;

  selectedTopCategoryId: number;
  selectedSubCategoryId: number;

  /** Category step component */
  @ViewChild(AuctionAddCategoryWizardStepComponent) categoryStep: { categoryStepForm: FormGroup; };

  /** About step component */
  @ViewChild(AuctionAddAboutWizardStepComponent) aboutStep: { aboutStepForm: FormGroup; };

  bsConfig: Partial<BsDatepickerConfig>;

  steps = [0];
  step = 0;

  categoryStepForm: FormGroup;
  addStepForm: FormGroup;
  aboutStepForm: FormGroup;

  private categoryConstants = CategoryConstants;

  constructor(
    private auctionService: AuctionsService,
    private notificationService: NotificationsService,
    public bsModalRef: BsModalRef,
    private externalModalService: BsModalService,
  ) { }

  ngOnInit(): void {
    this.loadTopAndSubCategories();
  }

  ngAfterViewInit(): void {
    this.categoryStepForm = this.categoryStep.categoryStepForm;
  }

  /** Validates if wizard's category step form is valid */
  moveDirectionCategoryStep = (direction: MovingDirection): boolean => {
    return this.moveDirection(this.categoryStepForm, direction);
  }

  /** Validates if wizard's add step form is valid */
  moveDirectionAddStep = (direction: MovingDirection): boolean => {
    if (this.addStepForm !== undefined) {
      return this.moveDirection(this.addStepForm, direction);
    } else {
      return false;
    }
  }

  /** Adds additional add-wizard step to the whole wizard flow */
  addWizardStep(event: boolean) {
    this.step++;
    this.steps.push(this.step);
  }

  onTopCategoryChange(categoryId: number): void {
    this.selectedTopCategoryId = categoryId;
  }

  onSubCategoryChange(typeId: number): void {
    this.selectedSubCategoryId = typeId;
  }

  onClickNextReturnForm(formType: string, form: FormGroup): void {
    if (formType === 'add-form') {
      this.addStepForm = form;
    } else {
      this.aboutStepForm = form;
      this.onSubmit();
    }
  }

  onSubmit(): void {
    this.aboutStepForm = this.aboutStep.aboutStepForm;

    if (this.selectedTopCategoryId === this.categoryConstants.ITEM_CATEGORY) {
      this.setItemAuctionAddRequest();
    } else if (this.selectedTopCategoryId === this.categoryConstants.VEHICLE_CATEGORY) {
      this.setVehicleAuctionAddRequest();
    } else if (this.selectedTopCategoryId === this.categoryConstants.PROPERTY_CATEGORY) {
      this.setPropertyAuctionAddRequest();
    } else {
      // something is wrong!
    }
  }

  /** Avoid memory leaks here by cleaning up after ourselves */
  ngOnDestroy(): void {
    if (this.auctionAddSubscription) {
      this.auctionAddSubscription.unsubscribe();
    }
  }

  private moveDirection = (formOfStep: FormGroup, direction: MovingDirection): boolean => {
    return direction === MovingDirection.Backwards ? true : formOfStep.valid;
  }

  private loadTopAndSubCategories(): void {
    this.auctionAddSubscription = this.auctionService.categoriesWithTypes$()
      .pipe(startWith(new CategoriesWithTypesModel()))
      .subscribe(
        (result: CategoriesWithTypesModel) => {
          this.categories = result;
          this.auctionTypes = result.subCategories;
        },
        (error: string) => this.notificationService.error(error)
      );
  }

  private setItemAuctionAddRequest(): void {
    this.addAuctionRequest = {
      aboutAuction: this.setAboutAuctionDetails(),
      itemAuction: {
        itemModel: this.addStepForm.value.itemModel,
        itemManufacturingYear: this.addStepForm.value.itemManufacturingYear,
        itemConditionId: this.addStepForm.value.itemCondition,
        itemEvaluation: this.addStepForm.value.itemEvaluation,
        itemStartingPrice: this.addStepForm.value.itemStartingPrice
      },
      aboutAuctionCreator: this.setAboutAuctionCreatorDetails()
    };

    this.addAuctionRequest.aboutAuction.auctionName = this.addStepForm.value.itemName;

    this.makeRequest(this.addAuctionRequest);
  }

  private setVehicleAuctionAddRequest(): void {
    this.addAuctionRequest = {
      aboutAuction: this.setAboutAuctionDetails(),
      vehicleAuction: {
        vehicleMake: this.addStepForm.value.vehicleMake,
        vehicleModel: this.addStepForm.value.vehicleModel,
        vehicleManufacturingYear: this.addStepForm.value.vehicleManufacturingYear,
        vehicleRegistrationNumber: this.addStepForm.value.vehicleRegistrationNumber,
        vehicleIdentificationNumber: this.addStepForm.value.vehicleIdentificationNumber,
        vehicleInspectionActive: this.addStepForm.value.vehicleInspectionActive,
        vehicleTransmissionId: this.addStepForm.value.vehicleTransmission,
        vehicleFuelTypeId: this.addStepForm.value.vehicleFuelType,
        vehicleEngineSize: this.addStepForm.value.vehicleEngineSize,
        vehicleAxis: this.addStepForm.value.vehicleAxis,
        vehicleEvaluation: this.addStepForm.value.vehicleEvaluation
      },
      aboutAuctionCreator: this.setAboutAuctionCreatorDetails()
    };

    this.addAuctionRequest.aboutAuction.auctionName = this.addStepForm.value.vehicleName;

    this.makeRequest(this.addAuctionRequest);
  }

  private setPropertyAuctionAddRequest(): void {
    this.addAuctionRequest = {
      aboutAuction: this.setAboutAuctionDetails(),
      propertyAuction: {
        propertyCoordinates: this.addStepForm.value.propertyCoordinates,
        propertyRegionId: this.addStepForm.value.propertyRegion,
        propertyCadastreNumber: this.addStepForm.value.propertyCadastreNumber,
        propertyMeasurementValue: this.addStepForm.value.propertyMeasurementValue,
        propertyMeasurementTypeId: this.addStepForm.value.propertyMeasurementType,
        propertyAddress: this.addStepForm.value.propertyAddress,
        propertyFloorCount: this.addStepForm.value.propertyFloorCount,
        propertyRoomCount: this.addStepForm.value.propertyRoomCount,
        propertyEvaluation: this.addStepForm.value.propertyEvaluation
      },
      aboutAuctionCreator: this.setAboutAuctionCreatorDetails()
    };

    this.addAuctionRequest.aboutAuction.auctionName = this.addStepForm.value.propertyName;

    this.makeRequest(this.addAuctionRequest);
  }

  private setAboutAuctionDetails(): Auctions.AboutAuctionModel {
    return {
      auctionTopCategoryId: this.categoryStepForm.value.auctionTopCategory,
      auctionSubCategoryId: this.categoryStepForm.value.auctionSubCategory,
      auctionFormatId: this.categoryStepForm.value.auctionFormat,
      auctionName: '', // todo: kke: why this is just empty string here?
      auctionStartingPrice: this.addStepForm.value.auctionStartingPrice,
      auctionStartDate: this.aboutStepForm.value.auctionStartDate,
      auctionApplyTillDate: this.aboutStepForm.value.auctionApplyTillDate,
      auctionEndDate: this.aboutStepForm.value.auctionEndDate
    };
  }

  private setAboutAuctionCreatorDetails(): Auctions.AboutAuctionCreatorModel {
    return {
      auctionCreatorName: this.aboutStepForm.value.auctionCreator,
      auctionCreatorAddress: this.aboutStepForm.value.auctionAddress,
      auctionCreatorEmail: this.aboutStepForm.value.auctionCreatorEmail,
      auctionCreatorPhone: this.aboutStepForm.value.auctionCreatorPhone,
    };
  }

  private makeRequest(request: Auctions.AddAuctionRequestModel): void {
    this.auctionService.addAuction$(request)
      .subscribe((addSuccess: boolean) => {
        if (addSuccess) {
          this.notificationService.success('Auction successfully added.');
          this.bsModalRef.hide();
          this.externalModalService.setDismissReason('Create');
        } else {
          this.notificationService.error('Could not add auction.');
        }
      },
        (error: string) => this.notificationService.error(error));
  }
}
