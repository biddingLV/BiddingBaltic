// angular
import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';

// internal
import { FormService } from 'ClientApp/src/app/core/services/form/form.service';
import { TypeConstants } from 'ClientApp/src/app/core/constants/types/type-constants';


@Component({
  selector: 'app-auction-add-wizard-vehicle-category',
  templateUrl: './vehicle-category.component.html'
})
export class AuctionAddWizardVehicleComponent implements OnInit {
  @Input() selectedSubCategoryId: number;

  @Output() returnAddWizardStepForm = new EventEmitter<FormGroup>();

  /** Form what used in the template */
  addStepForm: FormGroup;

  submitted = false;

  /** Form error object */
  formErrors = {
    vehicleMake: '',
    vehicleModel: '',
    vehicleManufacturingDate: '',
    vehicleRegistrationNumber: '',
    vehicleIdentificationNumber: '',
    vehicleInspectionActive: '',
    vehicleTransmission: '',
    vehicleFuelType: '',
    vehicleEngineSize: '',
    vehicleAxis: '',
    vehicleEvaluation: ''
  };

  typeConstants = TypeConstants;

  transmissionTypes = [
    {
      transmissionTypeId: 1,
      transmissionTypeName: 'automatiskā'
    },
    {
      transmissionTypeId: 2,
      transmissionTypeName: 'mehāniskā'
    }
  ]

  fuelTypes = [
    {
      fuelTypeId: 1,
      fuelTypeName: 'benzīns'
    },
    {
      fuelTypeId: 2,
      fuelTypeName: 'dīzelis'
    },
    {
      fuelTypeId: 3,
      fuelTypeName: 'benzīns/naftas gāze'
    },
    {
      fuelTypeId: 4,
      fuelTypeName: 'elektroniskais'
    },
    {
      fuelTypeId: 5,
      fuelTypeName: 'hibrīds'
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
      vehicleMake: ['BMW', []],
      vehicleModel: ['1-Series', []],
      vehicleManufacturingDate: [2014, []],
      vehicleRegistrationNumber: [5990144781, []],
      vehicleIdentificationNumber: [68813321, []],
      vehicleInspectionActive: ['', []],
      vehicleTransmission: ['', []],
      vehicleFuelType: ['', []],
      vehicleEngineSize: ['', []],
      vehicleAxis: ['', []],
      vehicleEvaluation: ['Garš teksts', []]
    });
  }
}
