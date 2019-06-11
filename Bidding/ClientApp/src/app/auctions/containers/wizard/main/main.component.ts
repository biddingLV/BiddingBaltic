// angular
import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

// 3rd party
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';
import { startWith } from 'rxjs/operators';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker/bs-datepicker.config';
import { MovingDirection } from 'angular-archwizard';

// internal
import { AuctionsService } from '../../../services/auctions.service';
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';
import { AuctionAddCategoryWizardStepComponent } from '../../../components/wizard/wizard-steps/category-step/category-step.component';
import { AuctionAddLastWizardStepComponent } from '../../../components/wizard/wizard-steps/last-step/last-step.component';
import { CategoryConstants } from 'ClientApp/src/app/core/constants/categories/category-constants';
import { CategoriesWithTypesModel } from '../../../models/add/categories-with-types.model';
import { TypeModel } from '../../../models/shared/types/type.model';


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

  /** Last step component */
  @ViewChild(AuctionAddLastWizardStepComponent) lastStep: { lastStepForm: FormGroup; };

  bsConfig: Partial<BsDatepickerConfig>;

  steps = [0];
  step = 0;

  categoryStepForm: FormGroup;
  addStepForm: FormGroup;
  lastStepForm: FormGroup;

  private categoryConstants = CategoryConstants;

  constructor(
    private auctionApi: AuctionsService,
    private notification: NotificationsService,
    public bsModalRef: BsModalRef
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

  onClickNextReturnForm(form: FormGroup): void {
    this.addStepForm = form;
  }

  onSubmit(): void {
    this.lastStepForm = this.lastStep.lastStepForm;
    console.log('this.categoryStepForm: ', this.categoryStepForm)
    console.log('this.addStepForm: ', this.addStepForm)
    console.log('this.lastStepForm: ', this.lastStepForm)

    if (this.selectedTopCategoryId == this.categoryConstants.ITEM_CATEGORY) {
      this.setItemAuctionAddRequest();
    } else if (this.selectedTopCategoryId == this.categoryConstants.VEHICLE_CATEGORY) {
      this.setVehicleAuctionAddRequest();
    } else if (this.selectedTopCategoryId == this.categoryConstants.OBJECT_CATEGORY) {
      this.setPropertyAuctionAddRequest();
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
    this.addAuctionRequest = {
      itemAuction: {
        auctionTopCategoryId: this.categoryStepForm.value.auctionTopCategory,
        auctionSubCategoryId: this.categoryStepForm.value.auctionSubCategory,
        itemName: this.addStepForm.value.itemName,
        itemModel: this.addStepForm.value.itemModel,
        itemManufacturingDate: this.addStepForm.value.itemManufacturingDate,
        itemEvaluation: this.addStepForm.value.itemEvaluation,
        itemStartingPrice: this.addStepForm.value.itemStartingPrice
      }
    }

    console.log('this.addAuctionRequest.itemAuction: ', this.addAuctionRequest.itemAuction)

    this.makeRequest(this.addAuctionRequest);
  }

  private setVehicleAuctionAddRequest(): void {
    this.addAuctionRequest = {
      vehicleAuction: {
        auctionTopCategoryId: this.categoryStepForm.value.auctionTopCategory,
        auctionSubCategoryId: this.categoryStepForm.value.auctionSubCategory,
        vehicleMake: this.addStepForm.value.vehicleMake,
        vehicleModel: this.addStepForm.value.vehicleModel,
        vehicleManufacturingDate: this.addStepForm.value.vehicleManufacturingDate,
        vehicleRegistrationNumber: this.addStepForm.value.vehicleRegistrationNumber,
        vehicleIdentificationNumber: this.addStepForm.value.vehicleIdentificationNumber,
        vehicleInspectionActive: this.addStepForm.value.vehicleInspectionActive,
        vehiclePower: this.addStepForm.value.vehiclePower,
        vehicleEngineSize: this.addStepForm.value.vehicleEngineSize,
        vehicleFuelType: this.addStepForm.value.vehicleFuelType,
        vehicleTransmission: this.addStepForm.value.vehicleTransmission,
        vehicleGearbox: this.addStepForm.value.vehicleGearbox,
        vehicleEvaluation: this.addStepForm.value.vehicleEvaluation
      }
    }

    // todo: kke: all of these values are undefined here! - pass form object back to main component!
    console.log('this.addAuctionRequest.vehicleAuction: ', this.addAuctionRequest.vehicleAuction)

    this.makeRequest(this.addAuctionRequest);
  }

  private setPropertyAuctionAddRequest(): void {
    this.addAuctionRequest = {
      propertyAuction: {
        auctionTopCategoryId: this.categoryStepForm.value.auctionTopCategory,
        auctionSubCategoryId: this.categoryStepForm.value.auctionSubCategory,
        propertyCoordinates: this.addStepForm.value.objectCoordinates,
        propertyRegion: this.addStepForm.value.objectRegion
      }
    }

    // todo: kke: all of these values are undefined here! - pass form object back to main component!
    console.log('this.addAuctionRequest.propertyAuction: ', this.addAuctionRequest.propertyAuction)

    this.makeRequest(this.addAuctionRequest);
  }

  private makeRequest(request: Auctions.AddAuctionRequestModel): void {
    this.auctionApi.addAuction$(request)
      .subscribe((response: boolean) => {
        if (response) {
          this.notification.success('Auction successfully added.');
          this.bsModalRef.hide();
        } else {
          this.notification.error('Could not add auction.');
        }
      },
        (error: string) => this.notification.error(error));
  }
}
