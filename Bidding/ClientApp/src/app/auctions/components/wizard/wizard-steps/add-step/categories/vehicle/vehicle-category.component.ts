// angular
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';

// internal


@Component({
  selector: 'app-auction-add-wizard-vehicle-category',
  templateUrl: './vehicle-category.component.html'
})
export class AuctionAddWizardVehicleComponent implements OnInit {
  /** Form what used in the template */
  addVehicleForm: FormGroup;

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
  get f() { return this.addVehicleForm.controls; }

  constructor(
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.buildForm();
  }

  private buildForm(): void {
    this.addVehicleForm = this.fb.group({
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
