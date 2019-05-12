// angular
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray } from '@angular/forms';


@Component({
  selector: 'app-auction-add-last-wizard-step',
  templateUrl: './last-step.component.html'
})
export class AuctionAddLastWizardStepComponent implements OnInit {
  /** Form what used in the template */
  lastStepForm: FormGroup;

  /** Form error object */
  formErrors = {

  };

  constructor(
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.buildForm();
  }

  private buildForm(): void {
    this.lastStepForm = this.fb.group({
      auctionCreator: ['', []],
      auctionFormat: ['', []],
      auctionStartDate: ['', []],
      auctionApplyTillDate: ['', []],
      auctionEndDate: ['', []]
    });
  }
}
