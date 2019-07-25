// angular
import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

// 3rd lib
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker/bs-datepicker.config';
import { BsModalRef } from 'ngx-bootstrap/modal';

// internal
import { FormService } from 'ClientApp/src/app/core/services/form/form.service';


@Component({
  selector: 'app-auction-add-about-wizard-step',
  templateUrl: './about-step.component.html'
})
export class AuctionAddAboutWizardStepComponent implements OnInit {
  @Output() returnAddWizardStepForm = new EventEmitter<FormGroup>();

  /** Form what used in the template */
  aboutStepForm: FormGroup;

  /** Form error object */
  formErrors = {
    auctionCreator: '',
    auctionAddress: '',
    auctionCreatorEmail: '',
    auctionCreatorPhone: '',
    auctionStartDate: '',
    auctionApplyTillDate: '',
    auctionEndDate: ''
  };

  bsConfig: Partial<BsDatepickerConfig>;

  submitted = false;

  /** Convenience getter for easy access to form fields */
  get f() { return this.aboutStepForm.controls; }

  constructor(
    private formBuilder: FormBuilder,
    public bsModalRef: BsModalRef,
    private internalFormService: FormService
  ) {
    this.bsConfig = {
      containerClass: 'theme-green',
      dateInputFormat: 'DD/MM/YYYY',
      showWeekNumbers: true
    };
  }

  ngOnInit(): void {
    this.buildForm();
  }

  onNext(): void {
    this.submitted = true;

    // mark all fields as touched
    this.internalFormService.markFormGroupTouched(this.aboutStepForm);

    if (this.aboutStepForm.valid === false) {
      this.formErrors = this.internalFormService.validateForm(this.aboutStepForm, this.formErrors, false);
    }

    // stop here if form is invalid
    if (this.aboutStepForm.invalid) {
      return;
    }

    // return form values back to parent component
    this.returnAddWizardStepForm.emit(this.aboutStepForm);
  }

  private buildForm(): void {
    this.aboutStepForm = this.formBuilder.group({
      auctionCreator: ['', [Validators.required]],
      auctionAddress: ['', [Validators.required]],
      auctionCreatorEmail: ['', [Validators.required]],
      auctionCreatorPhone: ['', [Validators.required]],
      auctionStartDate: [null, []],
      auctionApplyTillDate: [null, [Validators.required]],
      auctionEndDate: [null, [Validators.required]]
    });
  }
}
