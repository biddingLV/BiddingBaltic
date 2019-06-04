// angular
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';

// internal
import { FormService } from 'ClientApp/src/app/core/services/form/form.service';


@Component({
  selector: 'app-auction-add-wizard-estate-category',
  templateUrl: './estate-category.component.html'
})
export class AuctionAddWizardEstateComponent implements OnInit {
  @Output() returnAddWizardStepForm = new EventEmitter<FormGroup>();

  /** Form what used in the template */
  addStepForm: FormGroup;

  submitted = false;

  /** Form error object */
  formErrors = {
    objectCoordinates: '',
    objectRegion: ''
  };

  /** Convenience getter for easy access to form fields */
  get f() { return this.addStepForm.controls; }

  constructor(
    private formBuilder: FormBuilder,
    private internalFormService: FormService
  ) { }

  ngOnInit(): void {
    this.buildForm();
  }

  private buildForm(): void {
    this.addStepForm = this.formBuilder.group({
      objectCoordinates: ['41 24.2028, 2 10.4418', []],
      objectRegion: ['Midtjylland', []]
    });
  }
}
