// angular
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray } from '@angular/forms';

// 3rd lib
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker/bs-datepicker.config';
import { BsModalRef } from 'ngx-bootstrap/modal';


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

  bsConfig: Partial<BsDatepickerConfig>;

  constructor(
    private fb: FormBuilder,
    public bsModalRef: BsModalRef
  ) { }

  ngOnInit(): void {
    this.buildForm();
  }

  onSubmit(): void {

  }

  private buildForm(): void {
    this.lastStepForm = this.fb.group({
      auctionCreator: ['', []],
      auctionAddress: ['', []],
      auctionCreatorEmail: ['', []],
      auctionCreatorPhone: ['', []],
      auctionFormat: ['', []],
      auctionStartDate: ['', []],
      auctionApplyTillDate: ['', []],
      auctionEndDate: ['', []]
    });
  }
}
