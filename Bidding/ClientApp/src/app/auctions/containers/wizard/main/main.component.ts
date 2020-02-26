// angular
import {
  Component,
  OnInit,
  ViewChild,
  AfterViewInit,
  OnDestroy
} from "@angular/core";
import { FormGroup } from "@angular/forms";

// 3rd party
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { Subscription } from "rxjs";
import { startWith, concatMap } from "rxjs/operators";
import { BsDatepickerConfig } from "ngx-bootstrap/datepicker/bs-datepicker.config";
import { MovingDirection } from "angular-archwizard";
import * as moment from "moment-mini";

// internal
import { AuctionsService } from "../../../services/auctions.service";
import { NotificationsService } from "ClientApp/src/app/core/services/notifications/notifications.service";
import { AuctionAddCategoryWizardStepComponent } from "../../../components/wizard/wizard-steps/category-step/category-step.component";
import { CategoriesWithTypesModel } from "../../../models/add/categories-with-types.model";
import { TypeModel } from "../../../models/shared/types/type.model";
import { AuctionAddAboutWizardStepComponent } from "../../../components/wizard/wizard-steps/about-step/about-step.component";
import { FileUploaderService } from "ClientApp/src/app/shared/services/file-uploader/file-uploader.service";
import { AuctionTopCategoryIds } from "ClientApp/src/app/core/constants/auction-top-category-constants";
import { AddAuctionRequestModel } from "../../../models/add/add-auction-request.model";
import { AboutAuctionCreatorModel } from "../../../models/add/about-auction-creator.model";
import { AboutAuctionModel } from "../../../models/add/about-auction.model";

@Component({
  templateUrl: "./main.component.html"
})
export class AuctionAddMainWizardComponent
  implements OnInit, AfterViewInit, OnDestroy {
  // component
  auctionAddSubscription: Subscription;
  submitted = false;

  // filters
  categories: CategoriesWithTypesModel;
  auctionTypes: TypeModel[];

  // API
  addAuctionRequest: AddAuctionRequestModel;

  // template
  selectedTopCategoryId: number;
  selectedSubCategoryId: number;
  selectedFormatId: number;

  /** Category step component */
  @ViewChild(AuctionAddCategoryWizardStepComponent, { static: true })
  categoryStep: { categoryStepForm: FormGroup };

  /** About step component */
  @ViewChild(AuctionAddAboutWizardStepComponent, { static: true }) aboutStep: {
    aboutStepForm: FormGroup;
  };

  bsConfig: Partial<BsDatepickerConfig>;

  steps = [0];
  step = 0;

  categoryStepForm: FormGroup;
  addStepForm: FormGroup;
  aboutStepForm: FormGroup;

  constructor(
    private auctionService: AuctionsService,
    private notificationService: NotificationsService,
    public bsModalRef: BsModalRef,
    private externalModalService: BsModalService,
    private fileUploaderService: FileUploaderService
  ) {}

  ngOnInit(): void {
    this.loadTopAndSubCategories();
  }

  ngAfterViewInit(): void {
    this.categoryStepForm = this.categoryStep.categoryStepForm;
  }

  /** Validates if wizard's category step form is valid */
  moveDirectionCategoryStep = (direction: MovingDirection): boolean => {
    return this.moveDirection(this.categoryStepForm, direction);
  };

  /** Validates if wizard's add step form is valid */
  moveDirectionAddStep = (direction: MovingDirection): boolean => {
    if (this.addStepForm !== undefined) {
      return this.moveDirection(this.addStepForm, direction);
    } else {
      return false;
    }
  };

  /** Adds additional add-wizard step to the whole wizard flow */
  addWizardStep(event: boolean): void {
    this.step++;
    this.steps.push(this.step);
  }

  onTopCategoryChange(categoryId: number): void {
    this.selectedTopCategoryId = categoryId;
  }

  onSubCategoryChange(typeId: number): void {
    this.selectedSubCategoryId = typeId;
  }

  onAuctionFormatChange(formatId: number): void {
    this.selectedFormatId = formatId;
  }

  onClickNextReturnForm(formType: string, form: FormGroup): void {
    if (formType === "add-form") {
      // note: this sets middle step form values, dont call onSubmit here!
      this.addStepForm = form;
      return;
    }

    if (formType === "about-form") {
      this.aboutStepForm = form;
      this.onSubmit();
    }

    return;
  }

  onSubmit(): void {
    this.aboutStepForm = this.aboutStep.aboutStepForm;

    if (this.selectedTopCategoryId === AuctionTopCategoryIds.ItemCategoryId) {
      this.setItemAuctionAddRequest();
    }

    if (
      this.selectedTopCategoryId === AuctionTopCategoryIds.VehicleCategoryId
    ) {
      this.setVehicleAuctionAddRequest();
    }

    if (
      this.selectedTopCategoryId === AuctionTopCategoryIds.PropertyCategoryId
    ) {
      this.setPropertyAuctionAddRequest();
    }

    return;
  }

  /** Avoid memory leaks here by cleaning up after ourselves */
  ngOnDestroy(): void {
    if (this.auctionAddSubscription) {
      this.auctionAddSubscription.unsubscribe();
    }
  }

  private moveDirection = (
    formOfStep: FormGroup,
    direction: MovingDirection
  ): boolean => {
    return direction === MovingDirection.Backwards ? true : formOfStep.valid;
  };

  private loadTopAndSubCategories(): void {
    this.auctionAddSubscription = this.auctionService
      .categoriesWithTypes$()
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
    let addStepForm = this.addStepForm.value;

    this.addAuctionRequest = {
      aboutAuction: this.setAboutAuctionDetails(),
      itemAuction: {
        itemModel: addStepForm.itemModel,
        itemManufacturingYear: addStepForm.itemManufacturingYear,
        itemConditionId: addStepForm.itemCondition,
        itemEvaluation: addStepForm.itemEvaluation,
        itemStartingPrice: addStepForm.itemStartingPrice,
        itemVolume: addStepForm.itemVolume,
        itemCompanyTypeId: addStepForm.itemCompanyType
      },
      aboutAuctionCreator: this.setAboutAuctionCreatorDetails()
    };

    this.addAuctionRequest.aboutAuction.auctionName = addStepForm.itemName;

    this.makeRequest(this.addAuctionRequest);
  }

  private setVehicleAuctionAddRequest(): void {
    let addStepForm = this.addStepForm.value;

    this.addAuctionRequest = {
      aboutAuction: this.setAboutAuctionDetails(),
      vehicleAuction: {
        vehicleMake: addStepForm.vehicleMake,
        vehicleModel: addStepForm.vehicleModel,
        vehicleManufacturingYear: addStepForm.vehicleManufacturingYear,
        vehicleRegistrationNumber: addStepForm.vehicleRegistrationNumber,
        vehicleIdentificationNumber: addStepForm.vehicleIdentificationNumber,
        vehicleInspectionActive: addStepForm.vehicleInspectionActive,
        vehicleTransmissionId: addStepForm.vehicleTransmission,
        vehicleFuelTypeId: addStepForm.vehicleFuelType,
        vehicleEngineSize: addStepForm.vehicleEngineSize,
        vehicleAxis: addStepForm.vehicleAxis,
        vehicleDimensionValue: addStepForm.vehicleDimensionValue,
        vehicleDimensionType: addStepForm.vehicleDimensionType,
        vehicleEvaluation: addStepForm.vehicleEvaluation
      },
      aboutAuctionCreator: this.setAboutAuctionCreatorDetails()
    };

    this.addAuctionRequest.aboutAuction.auctionName = addStepForm.vehicleName;

    this.makeRequest(this.addAuctionRequest);
  }

  private setPropertyAuctionAddRequest(): void {
    let addStepForm = this.addStepForm.value;

    this.addAuctionRequest = {
      aboutAuction: this.setAboutAuctionDetails(),
      propertyAuction: {
        propertyCoordinates: addStepForm.propertyCoordinates,
        propertyRegionId: addStepForm.propertyRegion,
        propertyCadastreNumber: addStepForm.propertyCadastreNumber,
        propertyMeasurementValue: addStepForm.propertyMeasurementValue,
        propertyMeasurementTypeId: addStepForm.propertyMeasurementType,
        propertyAddress: addStepForm.propertyAddress,
        propertyFloorCount: addStepForm.propertyFloorCount,
        propertyRoomCount: addStepForm.propertyRoomCount,
        propertyEvaluation: addStepForm.propertyEvaluation
      },
      aboutAuctionCreator: this.setAboutAuctionCreatorDetails()
    };

    this.addAuctionRequest.aboutAuction.auctionName = addStepForm.propertyName;

    this.makeRequest(this.addAuctionRequest);
  }

  private setAboutAuctionDetails(): AboutAuctionModel {
    let categoryStepValues = this.categoryStepForm.value;
    let addStepValues = this.addStepForm.value;
    let aboutStepValues = this.aboutStepForm.value;

    return {
      auctionTopCategoryId: categoryStepValues.auctionTopCategory,
      auctionSubCategoryId: categoryStepValues.auctionSubCategory,
      auctionFormatId: categoryStepValues.auctionFormat,
      auctionName: "", // todo: kke: why this is just empty string here?
      auctionStartingPrice: addStepValues.auctionStartingPrice,
      auctionValueAddedTax: addStepValues.auctionValueAddedTax,
      auctionStartDate: this.combineDateWithTime(
        aboutStepValues.auctionStartDate,
        aboutStepValues.auctionStartTime
      ),
      auctionApplyTillDate: this.combineDateWithTime(
        aboutStepValues.auctionApplyTillDate,
        aboutStepValues.auctionApplyTillTime
      ),
      auctionEndDate: this.combineDateWithTime(
        aboutStepValues.auctionEndDate,
        aboutStepValues.auctionEndTime
      )
    };
  }

  /**
   * Combines two dates, one from datepicker, second from timepicker!
   * @param dateValue Only interested in date part!
   * @param timeValue Only interesred in time part!
   */
  private combineDateWithTime(dateValue: Date, timeValue: Date): Date {
    const dateFormat = "YYYY-MM-DD";
    const timeFormat = "HH:mm:ss";

    let date = moment(dateValue).format(dateFormat);
    let time = moment(timeValue).format(timeFormat);

    return moment(date + " " + time, "YYYY-MM-DD hh:mm:ss").toDate();
  }

  private setAboutAuctionCreatorDetails(): AboutAuctionCreatorModel {
    let aboutForm = this.aboutStepForm.value;

    return {
      auctionCreatorName: aboutForm.auctionCreator,
      auctionCreatorAddress: aboutForm.auctionAddress,
      auctionRequirements: aboutForm.auctionRequirements,
      auctionCreatorEmail: aboutForm.auctionCreatorEmail,
      auctionCreatorPhone: aboutForm.auctionCreatorPhone,
      auctionExternalWebsite: aboutForm.auctionExternalWebsite
    };
  }

  private makeRequest(request: AddAuctionRequestModel): void {
    this.auctionAddSubscription = this.auctionService
      .addAuction$(request)
      .pipe(
        concatMap((auctionId: number) =>
          this.fileUploaderService.uploadFiles$(
            this.addStepForm.value.auctionFiles,
            auctionId
          )
        )
      )
      .subscribe(
        response => {
          if (response) {
            this.notificationService.success("Auction successfully added.");
            this.bsModalRef.hide();
            this.externalModalService.setDismissReason("Create");
          } else {
            this.notificationService.error("Could not create auction.");
          }
        },
        (error: string) => this.notificationService.error(error)
      );
  }
}
