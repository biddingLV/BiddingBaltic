// angular
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

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
    propertyName: '',
    auctionStartingPrice: '',
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

  regions = [
    {
      regionId: 1,
      regionName: 'Jelgava'
    },
    {
      regionId: 2,
      regionName: 'Ogre'
    },
    {
      regionId: 3,
      regionName: 'Work in progress!'
    }
  ]

  measurementTypes = [
    {
      measurementTypeId: 1,
      measurementTypeName: 'm2'
    },
    {
      measurementTypeId: 2,
      measurementTypeName: 'a'
    },
    {
      measurementTypeId: 3,
      measurementTypeName: 'ha'
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
      propertyName: ['', [Validators.required]],
      auctionStartingPrice: [null, [Validators.required]],
      propertyCoordinates: ['', []],
      propertyRegion: [null, [Validators.required]],
      propertyCadastreNumber: [null, [Validators.required]],
      propertyMeasurementValue: [null, [Validators.required]],
      propertyMeasurementType: [null, [Validators.required]],
      propertyAddress: ['', []],
      propertyFloorCount: [null, []],
      propertyRoomCount: [null, []],
      propertyEvaluation: ['', []]
    });
  }
}
