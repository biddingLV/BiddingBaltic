// angular
import {
  Component,
  OnInit,
  EventEmitter,
  Output,
  OnChanges,
  SimpleChanges,
  Input,
  SimpleChange
} from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

// 3rd lib
import { BsModalRef } from "ngx-bootstrap/modal";
import * as moment from "moment-mini";
import { BsDatepickerConfig } from "ngx-bootstrap/datepicker/bs-datepicker.config";

// internal
import { FormService } from "ClientApp/src/app/core/services/form/form.service";
import { AuctionFormatConstants } from "ClientApp/src/app/core/constants/auction-format-constants";

@Component({
  selector: "app-auction-add-about-wizard-step",
  templateUrl: "./about-step.component.html"
})
export class AuctionAddAboutWizardStepComponent implements OnInit, OnChanges {
  @Input() selectedFormatId: number;

  /** Used to enable or disable action button in template */
  @Input() isDisabled: boolean = false;

  @Output() returnAddWizardStepForm = new EventEmitter<FormGroup>();

  /** Form what used in the template */
  aboutStepForm: FormGroup;

  /** Form error object */
  formErrors = {
    auctionCreator: "",
    auctionAddress: "",
    auctionRequirements: "",
    auctionCreatorEmail: "",
    auctionCreatorPhone: "",
    auctionExternalWebsite: "",
    auctionStartDate: "",
    auctionApplyTillDate: "",
    auctionEndDate: ""
  };

  submitted = false;

  /** datepicker settings */
  bsConfig: Partial<BsDatepickerConfig>;

  /** Default date with time for timepickers */
  defaultDateTime = moment(moment(), moment.ISO_8601).toDate();

  // template
  showWebsiteField: boolean;

  /** Convenience getter for easy access to form fields */
  get f() {
    return this.aboutStepForm.controls;
  }

  constructor(
    private formBuilder: FormBuilder,
    public bsModalRef: BsModalRef,
    private internalFormService: FormService
  ) {
    this.bsConfig = this.internalFormService.bsConfig;
  }

  ngOnChanges(changes: SimpleChanges): void {
    const formatChange = changes["selectedFormatId"];
    const isDisabledChange = changes["isDisabled"];

    if (formatChange != undefined) {
      this.handleFormatChange(formatChange);
    }

    if (isDisabledChange != undefined) {
      this.handleIsDisabledChange(isDisabledChange);
    }
  }

  ngOnInit(): void {
    this.buildForm();
  }

  onNext(): void {
    this.submitted = true;

    this.internalFormService.markFormGroupTouched(this.aboutStepForm);

    if (this.aboutStepForm.valid === false) {
      this.formErrors = this.internalFormService.validateForm(
        this.aboutStepForm,
        this.formErrors,
        false
      );
    }

    // stop here if form is invalid
    if (this.aboutStepForm.invalid) {
      return;
    }

    // return form values back to parent component
    this.returnAddWizardStepForm.emit(this.aboutStepForm);
  }

  onStartDateChange(time: Date): void {
    this.aboutStepForm.patchValue({
      auctionStartTime: time
    });
  }

  onApplyTillDateChange(time: Date): void {
    this.aboutStepForm.patchValue({
      auctionApplyTillTime: time
    });
  }

  onEndDateChange(time: Date): void {
    this.aboutStepForm.patchValue({
      auctionEndTime: time
    });
  }

  private buildForm(): void {
    this.aboutStepForm = this.formBuilder.group({
      auctionCreator: ["", [Validators.required]],
      auctionAddress: ["", [Validators.required]],
      auctionRequirements: ["", []],
      auctionCreatorEmail: ["", [Validators.required]],
      auctionCreatorPhone: ["", [Validators.required]],
      auctionExternalWebsite: [null, []],
      auctionStartDate: [null, []],
      auctionStartTime: [null, []],
      auctionApplyTillDate: [this.defaultDateTime, [Validators.required]],
      auctionApplyTillTime: [this.defaultDateTime, []],
      auctionEndDate: [this.defaultDateTime, [Validators.required]],
      auctionEndTime: [this.defaultDateTime, []]
    });
  }

  private handleFormatChange(change: SimpleChange) {
    if (change && change.isFirstChange() == false) {
      if (change.currentValue ==
        AuctionFormatConstants.AUCTION_ELECTRONICALLY_ID) {
        this.showWebsiteField = true;
      }
      else {
        this.showWebsiteField = false;
      }
    }
  }

  private handleIsDisabledChange(change: SimpleChange) {
    if (change && change.isFirstChange() == false) {
      this.isDisabled = change.currentValue;
    }
  }
}
