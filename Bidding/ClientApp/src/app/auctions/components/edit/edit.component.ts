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
import { AuctionItemModel } from '../../models/shared/auction-item.model';


@Component({
  selector: 'app-auction-edit',
  templateUrl: './edit.component.html'
})
export class AuctionEditComponent implements OnInit {
  /** Passed from parent component */
  selectedAuction: AuctionItemModel;

  // form
  auctionEditForm: FormGroup;
  submitted = false;
  formErrors = {
    auctionName: '',
    auctionStartingPrice: '',
    auctionStartDate: '',
    auctionEndDate: '',
    auctionStatusName: ''
  };

  // API
  auctionEditRequest: AuctionEditRequest;

  // convenience getter for easy access to form fields
  get f() { return this.auctionEditForm.controls; }

  constructor(
    private auctionService: AuctionsService,
    private notificationService: NotificationsService,
    private fb: FormBuilder,
    private formService: FormService,
    public bsModalRef: BsModalRef
  ) { }

  ngOnInit(): void {
    this.buildForm();
  }

  onSubmit(): void {
    this.submitted = true;

    // mark all fields as touched
    this.formService.markFormGroupTouched(this.auctionEditForm);

    if (this.auctionEditForm.valid) {
      this.makeRequest();
    } else {
      this.formErrors = this.formService.validateForm(this.auctionEditForm, this.formErrors, false);
    }

    // stop here if form is invalid
    if (this.auctionEditForm.invalid) {
      return;
    }
  }

  private buildForm(): void {
    this.auctionEditForm = this.fb.group({
      auctionName: [this.selectedAuction.auctionName, [
        Validators.maxLength(100)
      ]],
      auctionStartingPrice: [this.selectedAuction.auctionStartingPrice, [
        Validators.maxLength(100)
      ]],
      auctionStartDate: [this.selectedAuction.auctionStartDate, [
        Validators.maxLength(100)
      ]],
      auctionEndDate: [this.selectedAuction.auctionEndDate, [
        Validators.maxLength(100)
      ]],
      auctionStatusName: [this.selectedAuction.auctionEndDate, [
        Validators.maxLength(100)
      ]]
    });

    // on each value change we call the validateForm function
    // We only validate form controls that are dirty, meaning they are touched
    // the result is passed to the formErrors object
    this.auctionEditForm.valueChanges.subscribe((data) => {
      this.formErrors = this.formService.validateForm(this.auctionEditForm, this.formErrors, true);
    });
  }

  private setEditRequest(): void {
    this.auctionEditRequest = {
      auctionId: this.selectedAuction.auctionId,
      auctionName: this.auctionEditForm.value.auctionName,
      auctionStartingPrice: this.auctionEditForm.value.auctionStartingPrice,
      auctionStartDate: this.auctionEditForm.value.auctionStartDate,
      auctionEndDate: this.auctionEditForm.value.auctionEndDate,
      auctionStatusName: this.auctionEditForm.value.auctionStatusName
    };
  }

  private makeRequest(): void {
    this.setEditRequest();

    this.auctionService.editAuction$(this.auctionEditRequest)
      .subscribe((data: boolean) => {
        let editSuccess = data;
        if (editSuccess) {
          this.notificationService.success('Auction successfully updated.');
          this.auctionEditForm.reset();
          this.bsModalRef.hide();
        } else {
          this.notificationService.error('Could not update auction.');
        }
      },
        (error: string) => this.notificationService.error(error));
  }
}
