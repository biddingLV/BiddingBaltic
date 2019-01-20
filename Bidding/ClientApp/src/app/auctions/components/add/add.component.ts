// angular
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

// 3rd party
import { BsModalRef } from 'ngx-bootstrap/modal';

// internal
import { AuctionsService } from '../../services/auctions.service';
import { FormService } from 'ClientApp/src/app/core/services/form/form.service';
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';
import { AuctionAddRequest } from '../../models/add/auction-add-request.model';

// internal


@Component({
  selector: 'app-auction-add',
  templateUrl: './add.component.html',
  styleUrls: []
})
export class AuctionAddComponent implements OnInit {
  // form
  auctionAddForm: FormGroup;
  submitted = false;
  formErrors = {
    auctionName: '',
    description: '',
    startingPrice: '',
    startDate: '',
    creator: ''
  };

  // API
  auctionAddRequest: AuctionAddRequest;

  // convenience getter for easy access to form fields
  get f() { return this.auctionAddForm.controls; }

  constructor(
    private auctionApi: AuctionsService,
    private notification: NotificationsService,
    private fb: FormBuilder,
    private formService: FormService,
    public bsModalRef: BsModalRef
  ) { }

  ngOnInit() {
    this.buildForm();
  }

  onSubmit() {
    this.submitted = true;
    let addSuccess: boolean;

    // mark all fields as touched
    this.formService.markFormGroupTouched(this.auctionAddForm);

    if (this.auctionAddForm.valid) {
      this.setAddRequest();

      this.auctionApi.addAuction$(this.auctionAddRequest)
        .subscribe((data: boolean) => {
          addSuccess = data;
          if (addSuccess) {
            this.notification.success('Auction successfully added.');
            this.auctionAddForm.reset();
            this.bsModalRef.hide();
          } else {
            this.notification.error('Could not add auction.');
          }
        },
          (error: string) => this.notification.error(error));
    } else {
      this.formErrors = this.formService.validateForm(this.auctionAddForm, this.formErrors, false);
    }

    // stop here if form is invalid
    if (this.auctionAddForm.invalid) {
      return;
    }
  }

  private buildForm() {
    this.auctionAddForm = this.fb.group({
      auctionName: ['', [
        Validators.maxLength(100)
      ]],
      description: ['', [
        Validators.maxLength(100)
      ]],
      startingPrice: [, [
        Validators.maxLength(100)
      ]],
      startDate: ['', [
        Validators.maxLength(100)
      ]],
      creator: ['', [
      ]]
    });

    // on each value change we call the validateForm function
    // We only validate form controls that are dirty, meaning they are touched
    // the result is passed to the formErrors object
    this.auctionAddForm.valueChanges.subscribe((data) => {
      this.formErrors = this.formService.validateForm(this.auctionAddForm, this.formErrors, true);
    });
  }

  private setAddRequest() {
    this.auctionAddRequest = {
      AuctionName: this.auctionAddForm.value.auctionName,
      Description: this.auctionAddForm.value.description,
      StartingPrice: this.auctionAddForm.value.startingPrice,
      // StartDate: this.auctionAddForm.value.startDate,
      Creator: this.auctionAddForm.value.creator
    };
  }
}
