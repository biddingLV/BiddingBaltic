// angular
import { Component, OnInit, Output, EventEmitter } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";

// internal
import { FormService } from "ClientApp/src/app/core/services/form/form.service";
import { CustomValidators } from "ClientApp/src/app/core/services/form/custom.validators";

@Component({
  selector: "app-auction-add-wizard-item-category",
  templateUrl: "./item-category.component.html"
})
export class AuctionAddWizardItemComponent implements OnInit {
  @Output() returnAddWizardStepForm = new EventEmitter<FormGroup>();

  /** Form what used in the template */
  addStepForm: FormGroup;

  submitted = false;

  /** Form error object */
  formErrors = {
    itemName: "",
    itemModel: "",
    itemManufacturingYear: "",
    itemCondition: "",
    itemEvaluation: "",
    auctionStartingPrice: ""
  };

  conditions = [
    {
      conditionId: 1,
      conditionName: "jauns"
    },
    {
      conditionId: 2,
      conditionName: "lietots"
    }
  ];

  // component
  auctionImages: File[];
  auctionDocuments: File[];

  /** Convenience getter for easy access to form fields */
  get f() {
    return this.addStepForm.controls;
  }

  constructor(
    private formBuilder: FormBuilder,
    private internalFormService: FormService
  ) {}

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
      itemName: [null, [Validators.required]],
      itemModel: [null, [Validators.required]],
      itemManufacturingYear: [
        null,
        [
          Validators.required,
          CustomValidators.validateOnlyYear,
          Validators.maxLength(4)
        ]
      ],
      itemCondition: [null, []],
      itemEvaluation: [null, []],
      auctionStartingPrice: [
        null,
        [Validators.required, CustomValidators.validatePrice]
      ],
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
}
