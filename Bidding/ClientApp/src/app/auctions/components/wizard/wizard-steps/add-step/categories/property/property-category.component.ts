// angular
import {
  Component,
  OnInit,
  Output,
  EventEmitter,
  Input,
  SimpleChanges,
  OnChanges
} from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";

// internal
import { FormService } from "ClientApp/src/app/core/services/form/form.service";
import { CustomValidators } from "ClientApp/src/app/core/services/form/custom.validators";
import { RegionsConstants } from "ClientApp/src/app/core/constants/regions/regions";
import { AuctionSubCategoryIds } from "ClientApp/src/app/core/constants/auction-sub-category-constants";

@Component({
  selector: "app-auction-add-wizard-property-category",
  templateUrl: "./property-category.component.html"
})
export class AuctionAddWizardPropertyComponent implements OnInit, OnChanges {
  @Input() selectedSubCategoryId: number;
  @Output() returnAddWizardStepForm = new EventEmitter<FormGroup>();

  /** Form what used in the template */
  addStepForm: FormGroup;

  submitted = false;

  /** Form error object */
  formErrors = {
    propertyName: "",
    auctionStartingPrice: "",
    propertyCoordinates: "",
    propertyRegion: "",
    propertyCadastreNumber: "",
    propertyMeasurementValue: "",
    propertyMeasurementType: "",
    propertyAddress: "",
    propertyFloorCount: "",
    propertyRoomCount: "",
    propertyEvaluation: ""
  };

  regions = RegionsConstants.names;

  // note: kke: if you change these ids, also change them in DB (PropertyMeasurementTypes)!
  measurementTypes = [
    {
      measurementTypeId: 1,
      measurementTypeName: "m2"
    },
    {
      measurementTypeId: 2,
      measurementTypeName: "a"
    },
    {
      measurementTypeId: 3,
      measurementTypeName: "ha"
    }
  ];

  // component
  auctionImages: File[];
  auctionDocuments: File[];

  // template
  showCountFields: boolean;

  /** Convenience getter for easy access to form fields */
  get f() {
    return this.addStepForm.controls;
  }

  constructor(
    private formBuilder: FormBuilder,
    private internalFormService: FormService
  ) {}

  ngOnChanges(changes: SimpleChanges): void {
    const subCategoryChange = changes["selectedSubCategoryId"];

    if (subCategoryChange && !subCategoryChange.isFirstChange()) {
      if (subCategoryChange !== undefined) {
        this.handleCountFields();
      }
    }
  }

  ngOnInit(): void {
    this.buildForm();
  }

  /** On Next click validate if all the required values are specified */
  onNext(): void {
    this.submitted = true;

    this.internalFormService.markFormGroupTouched(this.addStepForm);

    if (this.addStepForm.valid === false) {
      this.formErrors = this.internalFormService.validateForm(
        this.addStepForm,
        this.formErrors,
        false
      );
    }

    this.handleUploadedFiles();

    // stop here if form is invalid
    if (this.addStepForm.invalid) {
      return;
    }

    // return form values back to parent component
    this.returnAddWizardStepForm.emit(this.addStepForm);
  }

  onImageChange(files: File[]): void {
    this.auctionImages = files;
  }

  onFileChange(files: File[]): void {
    this.auctionDocuments = files;
  }

  private buildForm(): void {
    this.addStepForm = this.formBuilder.group({
      propertyName: [null, [Validators.required]],
      auctionStartingPrice: [
        null,
        [Validators.required, CustomValidators.validatePrice]
      ],
      propertyCoordinates: [null, []],
      propertyRegion: [null, [Validators.required]],
      propertyCadastreNumber: [
        null,
        [Validators.required, CustomValidators.validateCadastreNumber]
      ],
      propertyMeasurementValue: [
        null,
        [Validators.required, CustomValidators.validateMeasurementValue]
      ],
      propertyMeasurementType: [null, [Validators.required]],
      propertyAddress: [null, []],
      propertyFloorCount: [null, []],
      propertyRoomCount: [null, []],
      propertyEvaluation: [null, []],
      auctionFiles: [null, []]
    });
  }

  private handleUploadedFiles(): void {
    const formData = new FormData();

    if (this.auctionImages && this.auctionImages.length > 0)
      this.internalFormService.addFilesToFormData(formData, this.auctionImages);

    if (this.auctionDocuments && this.auctionDocuments.length > 0)
      this.internalFormService.addFilesToFormData(
        formData,
        this.auctionDocuments
      );

    this.addStepForm.patchValue({
      auctionFiles: formData
    });
  }

  /**
   * Hides room & floor count in template for Land & Garage sub-categories
   */
  private handleCountFields(): void {
    this.showCountFields =
      this.selectedSubCategoryId == AuctionSubCategoryIds.LAND ||
      this.selectedSubCategoryId == AuctionSubCategoryIds.GARAGE
        ? false
        : true;
  }
}
