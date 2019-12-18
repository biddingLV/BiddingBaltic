// angular
import {
  Component,
  OnInit,
  Output,
  EventEmitter,
  SimpleChanges,
  Input
} from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";

// internal
import { FormService } from "ClientApp/src/app/core/services/form/form.service";
import { CustomValidators } from "ClientApp/src/app/core/services/form/custom.validators";
import { AuctionSubCategoryIds } from "ClientApp/src/app/core/constants/auction-sub-category-constants";

// TODO: kke: move this to model file!
export class AddItemFormFieldModel {
  showModelField: boolean;
  showYearField: boolean;
  showConditionField: boolean;
  showPictures: boolean;
  showVolumeField: boolean;
  showCompanyTypeField: boolean;

  constructor() {
    this.showModelField = true;
    this.showYearField = true;
    this.showConditionField = true;
    this.showPictures = true;
    this.showVolumeField = false;
    this.showCompanyTypeField = false;
  }
}

@Component({
  selector: "app-auction-add-wizard-item-category",
  templateUrl: "./item-category.component.html"
})
export class AuctionAddWizardItemComponent implements OnInit {
  @Input() selectedSubCategoryId: number;

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
    itemVolume: "",
    auctionStartingPrice: "",
    auctionValueAddedTax: ""
  };

  itemConditions = [
    {
      conditionId: 1,
      conditionName: "jauns"
    },
    {
      conditionId: 2,
      conditionName: "lietots"
    }
  ];

  companyTypes = [
    {
      companyTypeId: 1,
      companyTypeName: "SIA"
    },
    {
      companyTypeId: 2,
      companyTypeName: "A/S"
    }
  ];

  // component
  auctionImages: File[];
  auctionDocuments: File[];

  // template
  fieldConditions = new AddItemFormFieldModel();

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
        this.fieldConditions = new AddItemFormFieldModel();

        this.handleMaterials();
        this.handleCompanyShares();
        this.handleDomain();
        this.handleTradeMark();
        this.handleOtherItems();
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

  onImageChange(files: File[]): void {
    this.auctionImages = files;
  }

  onFileChange(files: File[]): void {
    this.auctionDocuments = files;
  }

  private buildForm(): void {
    this.addStepForm = this.formBuilder.group({
      itemName: [null, [Validators.required]],
      itemModel: [null, []], // TODO: KKE: add validations back - [Validators.required]
      itemManufacturingYear: [null, []], // TODO: KKE: add validations back - Validators.required, CustomValidators.validateOnlyYear, Validators.maxLength(4)
      itemCondition: [null, []],
      itemEvaluation: [null, []],
      auctionStartingPrice: [
        null,
        [Validators.required, CustomValidators.validatePrice]
      ],
      auctionValueAddedTax: [false, []],
      itemVolume: [null, []],
      itemCompanyType: [null, []],
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

  private handleMaterials(): void {
    if (
      this.selectedSubCategoryId == AuctionSubCategoryIds.PRODUCTION_MATS ||
      this.selectedSubCategoryId == AuctionSubCategoryIds.STORE_MATS
    ) {
      this.fieldConditions.showModelField = false;
      this.fieldConditions.showYearField = false;
      this.fieldConditions.showVolumeField = true;
    }
  }

  private handleCompanyShares(): void {
    if (this.selectedSubCategoryId == AuctionSubCategoryIds.COMPANY_SHARES) {
      this.fieldConditions.showModelField = false;
      this.fieldConditions.showYearField = false;
      this.fieldConditions.showConditionField = false;
      this.fieldConditions.showPictures = false;
      this.fieldConditions.showVolumeField = true;
      this.fieldConditions.showCompanyTypeField = true;
    }
  }

  private handleDomain(): void {
    if (this.selectedSubCategoryId == AuctionSubCategoryIds.DOMAIN) {
      this.fieldConditions.showModelField = false;
      this.fieldConditions.showConditionField = false;
      this.fieldConditions.showPictures = false;
    }
  }

  private handleTradeMark(): void {
    if (this.selectedSubCategoryId == AuctionSubCategoryIds.TRADE_MARK) {
      this.fieldConditions.showModelField = false;
      this.fieldConditions.showYearField = false;
      this.fieldConditions.showConditionField = false;
    }
  }

  private handleOtherItems(): void {
    if (this.selectedSubCategoryId == AuctionSubCategoryIds.OTHER_ITEMS) {
      this.fieldConditions.showConditionField = false;
    }
  }
}
