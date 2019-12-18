// angular
import {
  Component,
  OnInit,
  EventEmitter,
  Output,
  OnChanges,
  SimpleChanges,
  Input
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

  @Output() returnAddWizardStepForm = new EventEmitter<FormGroup>();

  /** Form what used in the template */
  aboutStepForm: FormGroup;

  /** Form error object */
  formErrors = {
    auctionCreator: "",
    auctionAddress: "",
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

    if (formatChange && !formatChange.isFirstChange()) {
      if (formatChange !== undefined) {
        if (
          formatChange.currentValue ==
          AuctionFormatConstants.AUCTION_ELECTRONICALLY_ID
        ) {
          this.showWebsiteField = true;
        } else {
          this.showWebsiteField = false;
        }
      }
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

  onStartDateChange(time: Date) {
    this.aboutStepForm.patchValue({
      auctionStartTime: time
    });
  }

  onApplyTillDateChange(time: Date) {
    this.aboutStepForm.patchValue({
      auctionApplyTillTime: time
    });
  }

  onEndDateChange(time: Date) {
    this.aboutStepForm.patchValue({
      auctionEndTime: time
    });
  }

  private buildForm(): void {
    this.aboutStepForm = this.formBuilder.group({
      auctionCreator: ["", [Validators.required]],
      auctionAddress: ["", [Validators.required]],
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
}
