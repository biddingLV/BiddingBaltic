// angular
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';

// internal
import { FormService } from 'ClientApp/src/app/core/services/form/form.service';
import { TypeConstants } from 'ClientApp/src/app/core/constants/types/type-constants';


@Component({
  selector: 'app-auction-add-wizard-property-category',
  templateUrl: './property-category.component.html'
})
export class AuctionAddWizardPropertyComponent implements OnInit {
  @Output() returnAddWizardStepForm = new EventEmitter<FormGroup>();

  /** Form what used in the template */
  addStepForm: FormGroup;

  submitted = false;

  /** Form error object */
  formErrors = {
    propertyCoordinates: '',
    propertyRegion: '',
    propertyCadastreNumber: '',
    propertyMeasurementValue: '',
    propertyMeasurementType: '',
    propertyAddress: '',
    propertyFloorCount: '',
    propertyRoomCount: '',
    propertyEvaluation: ''
  };

  typeConstants = TypeConstants;

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
      propertyCoordinates: [1234, []],
      propertyRegion: ['', []],
      propertyCadastreNumber: [477821332, []],
      propertyMeasurementValue: [100, []],
      propertyMeasurementType: ['', []],
      propertyAddress: ['Dummy address 1', []],
      propertyFloorCount: [3, []],
      propertyRoomCount: [12, []],
      propertyEvaluation: ['Midtjylland', []]
    });
  }
}
