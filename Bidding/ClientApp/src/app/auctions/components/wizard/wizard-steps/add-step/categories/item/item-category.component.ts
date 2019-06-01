// angular
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

// internal
import { FormService } from 'ClientApp/src/app/core/services/form/form.service';


@Component({
  selector: 'app-auction-add-wizard-item-category',
  templateUrl: './item-category.component.html'
})
export class AuctionAddWizardItemComponent implements OnInit {
  /** Form what used in the template */
  addItemForm: FormGroup;

  submitted = false;

  /** Form error object */
  formErrors = {
    itemName: '',
    itemModel: '',
    itemManufacturingDate: '',
    itemEvaluation: '',
    itemStartingPrice: ''
  };

  /** Convenience getter for easy access to form fields */
  get f() { return this.addItemForm.controls; }

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
    this.internalFormService.markFormGroupTouched(this.addItemForm);

    if (this.addItemForm.valid == false) {
      this.formErrors = this.internalFormService.validateForm(this.addItemForm, this.formErrors, false);
    }

    // stop here if form is invalid
    if (this.addItemForm.invalid) {
      return;
    }
  }

  private buildForm(): void {
    this.addItemForm = this.formBuilder.group({
      itemName: ['Jauns Audi', [Validators.required]],
      itemModel: ['A4', [Validators.required]],
      itemManufacturingDate: ['2017', []],
      itemCondition: ['Lietots', []],
      itemEvaluation: ['5000', [Validators.required]],
      itemStartingPrice: ['1500', []]
    });
  }
}
