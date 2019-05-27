// angular
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';

// internal


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
    itemCondition: '',
    evaluation: '',
    itemStartingPrice: ''
  };

  /** Convenience getter for easy access to form fields */
  get f() { return this.addItemForm.controls; }

  constructor(
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.buildForm();
  }

  private buildForm(): void {
    this.addItemForm = this.fb.group({
      itemName: ['', []],
      itemModel: ['', []],
      itemManufacturingDate: ['', []],
      itemCondition: ['', []],
      evaluation: ['', []],
      itemStartingPrice: ['', []]
    });
  }
}
