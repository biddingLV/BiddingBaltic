// angular
import { Component, OnInit, EventEmitter, Output } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

// 3rd lib
import { BsDatepickerConfig } from "ngx-bootstrap/datepicker/bs-datepicker.config";
import { BsModalRef } from "ngx-bootstrap/modal";
import * as moment from "moment-mini";

// internal
import { FormService } from "ClientApp/src/app/core/services/form/form.service";

@Component({
  selector: "app-auction-add-about-wizard-step",
  templateUrl: "./about-step.component.html"
})
export class AuctionAddAboutWizardStepComponent implements OnInit {
  @Output() returnAddWizardStepForm = new EventEmitter<FormGroup>();

  /** Form what used in the template */
  aboutStepForm: FormGroup;

  /** Form error object */
  formErrors = {
    auctionCreator: "",
    auctionAddress: "",
    auctionCreatorEmail: "",
    auctionCreatorPhone: "",
    auctionStartDate: "",
    auctionApplyTillDate: "",
    auctionEndDate: ""
  };

  bsConfig: Partial<BsDatepickerConfig>;

  submitted = false;

  /** Default date with time for timepickers */
  defaultDateTime = moment(moment(), moment.ISO_8601).toDate();

  /** Convenience getter for easy access to form fields */
  get f() {
    return this.aboutStepForm.controls;
  }

  constructor(
    private formBuilder: FormBuilder,
    public bsModalRef: BsModalRef,
    private internalFormService: FormService
  ) {
    this.bsConfig = {
      containerClass: "theme-green",
      dateInputFormat: "YYYY-MM-DD",
      showWeekNumbers: true,
      isAnimated: true,
      adaptivePosition: true
    };
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
      auctionStartDate: [null, []],
      auctionStartTime: [null, []],
      auctionApplyTillDate: [this.defaultDateTime, [Validators.required]],
      auctionApplyTillTime: [this.defaultDateTime, []],
      auctionEndDate: [this.defaultDateTime, [Validators.required]],
      auctionEndTime: [this.defaultDateTime, []]
    });
  }
}
