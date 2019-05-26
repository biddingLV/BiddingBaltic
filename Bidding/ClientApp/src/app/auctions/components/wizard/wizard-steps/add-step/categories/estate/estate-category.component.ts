// angular
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';

// internal


@Component({
  selector: 'app-auction-add-wizard-estate-category',
  templateUrl: './estate-category.component.html'
})
export class AuctionAddWizardEstateComponent implements OnInit {
  /** Form what used in the template */
  addEstateForm: FormGroup;

  submitted = false;

  /** Form error object */
  formErrors = {
    make: '',
    model: '',
    manufacturingDate: '',
    vehicleRegistrationNumber: '',
    vehicleIdentificationNumber: '',
    vehicleInspectionActive: '',
    power: '',
    engineSize: '',
    fuelType: '',
    transmission: '',
    gearbox: '',
    evaluation: ''
  };

  /** Convenience getter for easy access to form fields */
  get f() { return this.addEstateForm.controls; }

  constructor(
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.buildForm();
  }

  private buildForm(): void {
    this.addEstateForm = this.fb.group({
      make: ['', []],
      model: ['', []],
      manufacturingDate: ['', []],
      vehicleRegistrationNumber: ['', []],
      vehicleIdentificationNumber: ['', []],
      vehicleInspectionActive: ['', []],
      power: ['', []],
      engineSize: ['', []],
      fuelType: ['', []],
      transmission: ['', []],
      gearbox: ['', []],
      evaluation: ['', []]
    });
  }
}
