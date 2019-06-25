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
  auctionAddSub: Subscription;
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
    private auctionApi: AuctionsService,
    private notification: NotificationsService,
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
    if (formType == 'add-form') {
      this.addStepForm = form;
    } else {
      this.aboutStepForm = form;
      this.onSubmit();
    }
  }

  onSubmit(): void {
    this.aboutStepForm = this.aboutStep.aboutStepForm;
    console.log('this.categoryStepForm: ', this.categoryStepForm)
    console.log('this.addStepForm: ', this.addStepForm)
    console.log('this.aboutStepForm: ', this.aboutStepForm)

    if (this.selectedTopCategoryId == this.categoryConstants.ITEM_CATEGORY) {
      this.setItemAuctionAddRequest();
    } else if (this.selectedTopCategoryId == this.categoryConstants.VEHICLE_CATEGORY) {
      this.setVehicleAuctionAddRequest();
    } else if (this.selectedTopCategoryId == this.categoryConstants.PROPERTY_CATEGORY) {
      this.setPropertyAuctionAddRequest();
    } else {
      // something is wrong!
    }
  }

  private moveDirection = (formOfStep: FormGroup, direction: MovingDirection): boolean => {
    return direction === MovingDirection.Backwards ? true : formOfStep.valid;
  }

  private loadTopAndSubCategories(): void {
    this.auctionAddSub = this.auctionApi.categoriesWithTypes$()
      .pipe(startWith(new CategoriesWithTypesModel()))
      .subscribe(
        (result: CategoriesWithTypesModel) => {
          this.categories = result;
          this.auctionTypes = result.subCategories;
        },
        (error: string) => this.notification.error(error)
      );
  }

  private setItemAuctionAddRequest(): void {
    // this.addAuctionRequest = {
    //   auctionTopCategoryId: this.categoryStepForm.value.auctionTopCategory,
    //   auctionSubCategoryId: this.categoryStepForm.value.auctionSubCategory,
    //   itemAuction: {
    //     itemName: this.addStepForm.value.itemName,
    //     itemModel: this.addStepForm.value.itemModel,
    //     itemManufacturingDate: this.addStepForm.value.itemManufacturingDate,
    //     itemEvaluation: this.addStepForm.value.itemEvaluation,
    //     itemStartingPrice: this.addStepForm.value.itemStartingPrice
    //   }
    // }

    // console.log('this.addAuctionRequest.itemAuction: ', this.addAuctionRequest.itemAuction)

    this.makeRequest(this.addAuctionRequest);
  }

  private setVehicleAuctionAddRequest(): void {
    this.addAuctionRequest = {
      auctionTopCategoryId: this.categoryStepForm.value.auctionTopCategory,
      auctionSubCategoryId: this.categoryStepForm.value.auctionSubCategory,
      auctionName: this.addStepForm.value.auctionName,
      auctionStartingPrice: this.addStepForm.value.auctionStartingPrice,
      vehicleAuction: {
        vehicleMake: this.addStepForm.value.vehicleMake,
        vehicleModel: this.addStepForm.value.vehicleModel,
        vehicleManufacturingYear: this.addStepForm.value.vehicleManufacturingYear,
        vehicleRegistrationNumber: this.addStepForm.value.vehicleRegistrationNumber,
        vehicleIdentificationNumber: this.addStepForm.value.vehicleIdentificationNumber,
        vehicleInspectionActive: this.addStepForm.value.vehicleInspectionActive,
        vehicleTransmission: this.addStepForm.value.vehicleTransmission,
        vehicleFuelType: this.addStepForm.value.vehicleFuelType,
        vehicleEngineSize: this.addStepForm.value.vehicleEngineSize,
        vehicleAxis: this.addStepForm.value.vehicleAxis,
        vehicleEvaluation: this.addStepForm.value.vehicleEvaluation
      },
      aboutAuction: { // todo: kke: this can be refactored because it is the same for all top-categories!
        auctionCreator: this.aboutStepForm.value.auctionCreator,
        auctionAddress: this.aboutStepForm.value.auctionAddress,
        auctionCreatorEmail: this.aboutStepForm.value.auctionCreatorEmail,
        auctionCreatorPhone: this.aboutStepForm.value.auctionCreatorPhone,
        auctionFormat: this.aboutStepForm.value.auctionFormat,
        auctionStartDate: this.aboutStepForm.value.auctionStartDate,
        auctionApplyTillDate: this.aboutStepForm.value.auctionApplyTillDate,
        auctionEndDate: this.aboutStepForm.value.auctionEndDate
      }
    }

    this.makeRequest(this.addAuctionRequest);
  }

  private setPropertyAuctionAddRequest(): void {
    // this.addAuctionRequest = {
    //   auctionTopCategoryId: this.categoryStepForm.value.auctionTopCategory,
    //   auctionSubCategoryId: this.categoryStepForm.value.auctionSubCategory,
    //   propertyAuction: {
    //     propertyCoordinates: this.addStepForm.value.objectCoordinates,
    //     propertyRegion: this.addStepForm.value.objectRegion
    //   }
    // }

    // todo: kke: all of these values are undefined here! - pass form object back to main component!
    console.log('this.addAuctionRequest.propertyAuction: ', this.addAuctionRequest.propertyAuction)

    this.makeRequest(this.addAuctionRequest);
  }

  private makeRequest(request: Auctions.AddAuctionRequestModel): void {
    this.auctionApi.addAuction$(request)
      .subscribe((addSuccess: boolean) => {
        if (addSuccess) {
          this.notification.success('Auction successfully added.');
          this.bsModalRef.hide();
          this.externalModalService.setDismissReason('Create');
        } else {
          this.notification.error('Could not add auction.');
        }
      },
        (error: string) => this.notification.error(error));
  }
}
