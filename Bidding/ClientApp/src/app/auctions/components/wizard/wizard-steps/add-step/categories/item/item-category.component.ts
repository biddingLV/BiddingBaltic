// angular
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

// internal
import { FormService } from 'ClientApp/src/app/core/services/form/form.service';


@Component({
  selector: 'app-auction-add-wizard-item-category',
  templateUrl: './item-category.component.html'
})
export class AuctionAddWizardItemComponent implements OnInit {
  @Output() returnAddWizardStepForm = new EventEmitter<FormGroup>();

  /** Form what used in the template */
  addStepForm: FormGroup;

  submitted = false;

  /** Form error object */
  formErrors = {
    itemName: '',
    itemModel: '',
    itemManufacturingYear: '',
    itemCondition: '',
    itemEvaluation: '',
    auctionStartingPrice: ''
  };

  conditions = [
    {
      conditionId: 1,
      conditionName: 'jauns'
    },
    {
      conditionId: 2,
      conditionName: 'lietots'
    }
  ]

  /** Convenience getter for easy access to form fields */
  get f() { return this.addStepForm.controls; }

  constructor(
    private formBuilder: FormBuilder,
    private internalFormService: FormService
  ) { }

  ngOnInit(): void {
    this.buildForm();
  }

  /** On Next click validate if all the required values are specified */
  onNext(): void {
    this.submitted = true;

    // mark all fields as touched
    this.internalFormService.markFormGroupTouched(this.addStepForm);

    if (this.addStepForm.valid == false) {
      this.formErrors = this.internalFormService.validateForm(this.addStepForm, this.formErrors, false);
    }

    // stop here if form is invalid
    if (this.addStepForm.invalid) {
      return;
    }

    // return form values back to parent component
    this.returnAddWizardStepForm.emit(this.addStepForm);
  }
  
  private buildForm(): void {
    this.addStepForm = this.formBuilder.group({
      itemName: ['', [Validators.required]],
      itemModel: ['', [Validators.required]],
      itemManufacturingYear: [null, [Validators.required]],
      itemCondition: [null, []],
      itemEvaluation: ['', []],
      auctionStartingPrice: [null, [Validators.required]]
    });
  }
}
