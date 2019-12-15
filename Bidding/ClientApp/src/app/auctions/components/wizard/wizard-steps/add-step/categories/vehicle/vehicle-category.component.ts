// angular
import {
  Component,
  OnInit,
  Output,
  EventEmitter,
  Input,
  SimpleChanges
} from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";

// internal
import { FormService } from "ClientApp/src/app/core/services/form/form.service";
import { AuctionSubCategoryIds } from "ClientApp/src/app/core/constants/auction-sub-category-constants";
import { CustomValidators } from "ClientApp/src/app/core/services/form/custom.validators";

@Component({
  selector: "app-auction-add-wizard-vehicle-category",
  templateUrl: "./vehicle-category.component.html"
})
export class AuctionAddWizardVehicleComponent implements OnInit {
  @Input() selectedSubCategoryId: number;

  @Output() returnAddWizardStepForm = new EventEmitter<FormGroup>();

  /** Form what used in the template */
  addStepForm: FormGroup;

  submitted = false;

  /** Form error object */
  formErrors = {
    vehicleName: "",
    auctionStartingPrice: "",
    vehicleMake: "",
    vehicleModel: "",
    vehicleManufacturingYear: "",
    vehicleRegistrationNumber: "",
    vehicleIdentificationNumber: "",
    vehicleInspectionActive: "",
    vehicleTransmission: "",
    vehicleFuelType: "",
    vehicleEngineSize: "",
    vehicleAxis: "",
    vehicleDimensionValue: "",
    vehicleDimensionType: "",
    vehicleEvaluation: ""
  };

  // component
  auctionImages: File[];
  auctionDocuments: File[];

  auctionSubCategoryIds = AuctionSubCategoryIds;

  // note: kke: if you change these ids, also change them in DB (VehicleTransmissions)!
  transmissionTypes = [
    {
      transmissionTypeId: 1,
      transmissionTypeName: "automatiskā"
    },
    {
      transmissionTypeId: 2,
      transmissionTypeName: "mehāniskā"
    }
  ];

  // note: kke: if you change these ids, also change them in DB (VehicleFuelTypes)!
  fuelTypes = [
    {
      fuelTypeId: 1,
      fuelTypeName: "benzīns"
    },
    {
      fuelTypeId: 2,
      fuelTypeName: "dīzelis"
    },
    {
      fuelTypeId: 3,
      fuelTypeName: "benzīns/naftas gāze"
    },
    {
      fuelTypeId: 4,
      fuelTypeName: "elektroniskais"
    },
    {
      fuelTypeId: 5,
      fuelTypeName: "hibrīds"
    }
  ];

  // note: kke: if you change these ids, also change them in DB (-)!
  /** Only used for auctionSubCategoryIds.TRAILER category */
  dimensionValues = [
    {
      dimensionValueId: 1,
      dimensionValueName: "Garums"
    },
    {
      dimensionValueId: 2,
      dimensionValueName: "Platums"
    },
    {
      dimensionValueId: 3,
      dimensionValueName: "Augstums"
    }
  ];

  // template
  showTrailerFields: boolean;

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
        this.handleTrailerFields();
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

    // stop here if form is invalid
    if (this.addStepForm.invalid) {
      return;
    }

    this.handleUploadedFiles();

    // return form values back to parent component
    this.returnAddWizardStepForm.emit(this.addStepForm);
  }

  onImageChange(files: File[]) {
    this.auctionImages = files;
  }

  onFileChange(files: File[]) {
    this.auctionDocuments = files;
  }

  private buildForm(): void {
    this.addStepForm = this.formBuilder.group({
      vehicleName: [null, [Validators.required]],
      auctionStartingPrice: [
        null,
        [Validators.required, CustomValidators.validatePrice]
      ],
      vehicleMake: [null, [Validators.required]],
      vehicleModel: [null, [Validators.required]],
      vehicleManufacturingYear: [
        null,
        [
          Validators.required,
          CustomValidators.validateOnlyYear,
          Validators.maxLength(4)
        ]
      ],
      vehicleRegistrationNumber: [null, []],
      vehicleIdentificationNumber: [null, []],
      vehicleInspectionActive: [false, []],
      vehicleTransmission: [null, []],
      vehicleFuelType: [null, []],
      vehicleEngineSize: [null, []],
      vehicleAxis: [null, []],
      vehicleDimensionValue: [null, []],
      vehicleDimensionType: [null, []],
      vehicleEvaluation: [null, []],
      auctionFiles: [null, []]
    });
  }

  private handleUploadedFiles() {
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

  private handleTrailerFields(): void {
    this.showTrailerFields =
      this.selectedSubCategoryId == AuctionSubCategoryIds.TRAILER
        ? true
        : false;
  }
}
