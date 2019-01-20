// angular
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

// 3rd party
import { BsModalRef } from 'ngx-bootstrap/modal';

// internal
import { AuctionsService } from '../../services/auctions.service';
import { FormService } from 'ClientApp/src/app/core/services/form/form.service';
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';
import { AuctionEditRequest } from '../../models/edit/auction-edit-request.model';


@Component({
  selector: 'app-auction-edit',
  templateUrl: './edit.component.html',
  styleUrls: []
})
export class AuctionEditComponent implements OnInit {
  // info from parent component passed from initialState object
  auctionId: number; // todo: kke: add this to the parent component!
  auctionName: string;
  auctionPrice: number;
  auctionStartDate: string; // todo: kke: can this be a date?
  auctionEndDate: string; // todo: kke: can this be a date?
  auctionDescription: string;

  // form
  auctionEditForm: FormGroup;
  submitted = false;
  formErrors = {
    auctionName: '',
    description: '',
    startingPrice: '',
    startDate: '',
    creator: ''
  };

  // API
  auctionEditRequest: AuctionEditRequest;

  // convenience getter for easy access to form fields
  get f() { return this.auctionEditForm.controls; }

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
    this.formService.markFormGroupTouched(this.auctionEditForm);

    if (this.auctionEditForm.valid) {
      this.setAddRequest();

      this.auctionApi.editAuction$(this.auctionEditRequest)
        .subscribe((data: boolean) => {
          addSuccess = data;
          if (addSuccess) {
            this.notification.success('Auction successfully updated.');
            this.auctionEditForm.reset();
            this.bsModalRef.hide();
          } else {
            this.notification.error('Could not update auction.');
          }
        },
          (error: string) => this.notification.error(error));
    } else {
      this.formErrors = this.formService.validateForm(this.auctionEditForm, this.formErrors, false);
    }

    // stop here if form is invalid
    if (this.auctionEditForm.invalid) {
      return;
    }
  }

  private buildForm() {
    this.auctionEditForm = this.fb.group({
      auctionName: [this.auctionName, [
        Validators.maxLength(100)
      ]],
      description: [this.auctionDescription, [
        Validators.maxLength(100)
      ]],
      startingPrice: [this.auctionPrice, [
        Validators.maxLength(100)
      ]],
      startDate: [this.auctionStartDate, [
        Validators.maxLength(100)
      ]],
      creator: ['', [
      ]]
    });

    // on each value change we call the validateForm function
    // We only validate form controls that are dirty, meaning they are touched
    // the result is passed to the formErrors object
    this.auctionEditForm.valueChanges.subscribe((data) => {
      this.formErrors = this.formService.validateForm(this.auctionEditForm, this.formErrors, true);
    });
  }

  private setAddRequest() {
    this.auctionEditRequest = {
      AuctionId: 1,
      AuctionName: this.auctionEditForm.value.auctionName,
      Description: this.auctionEditForm.value.description,
      StartingPrice: this.auctionEditForm.value.startingPrice,
      // StartDate: this.auctionAddForm.value.startDate,
      Creator: this.auctionEditForm.value.creator
    };
  }
}
