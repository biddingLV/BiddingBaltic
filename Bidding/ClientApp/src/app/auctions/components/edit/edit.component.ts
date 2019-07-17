// angular
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

// 3rd party
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';

// internal
import { AuctionsService } from '../../services/auctions.service';
import { FormService } from 'ClientApp/src/app/core/services/form/form.service';
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';
import { AuctionEditRequest } from '../../models/edit/auction-edit-request.model';


@Component({
  selector: 'app-auction-edit',
  templateUrl: './edit.component.html'
})
export class AuctionEditComponent implements OnInit {
  /** Selected auction's id passed from parent component */
  selectedAuctionId: number;

  auctionEditSubscription: Subscription;

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

  /** API - request model */
  auctionEditRequest: AuctionEditRequest;

  // convenience getter for easy access to form fields
  get f() { return this.auctionEditForm.controls; }

  constructor(
    private auctionService: AuctionsService,
    private notificationService: NotificationsService,
    private formBuilder: FormBuilder,
    private formService: FormService,
    public bsModalRef: BsModalRef
  ) { }

  ngOnInit(): void {
    this.buildForm();
  }

  onSubmit(): void {
    this.submitted = true;

    this.formService.markFormGroupTouched(this.auctionEditForm);

    if (this.auctionEditForm.valid) {
      this.makeUpdateRequest();
    } else {
      this.formErrors = this.formService.validateForm(this.auctionEditForm, this.formErrors, false);
      return;
    }
  }

  private buildForm(): void {
    let auctionDetails = this.loadAuctionDetails();

    this.auctionEditForm = this.formBuilder.group({
      auctionName: ['', []],
      auctionStartingPrice: [null, []],
      auctionStartDate: [null, []],
      auctionApplyTillDate: [null, []],
      auctionEndDate: [null, []],
      auctionStatus: [null, []]
    });

    this.auctionEditForm.valueChanges.subscribe((data) => {
      this.formErrors = this.formService.validateForm(this.auctionEditForm, this.formErrors, true);
    });
  }

  private loadAuctionDetails() {
    this.auctionEditSubscription = this.auctionService
      .getAuctionEditDetails$(this.selectedAuctionId)
      .subscribe(
        (editDetails: any) => {

        },
        (error: string) => this.notificationService.error(error)
      );
  }

  private setEditRequest(): void {
    this.auctionEditRequest = {
      auctionId: this.selectedAuctionId,
      auctionName: this.auctionEditForm.value.auctionName,
      auctionStartingPrice: this.auctionEditForm.value.auctionStartingPrice,
      auctionStartDate: this.auctionEditForm.value.auctionStartDate,
      auctionApplyTillDate: this.auctionEditForm.value.auctionStartDate,
      auctionEndDate: this.auctionEditForm.value.auctionEndDate,
      auctionStatusId: this.auctionEditForm.value.auctionStatus
    };
  }

  private makeUpdateRequest(): void {
    this.setEditRequest();

    this.auctionService.editAuction$(this.auctionEditRequest)
      .subscribe((editSuccess: boolean) => {
        if (editSuccess) {
          this.notificationService.success('Auction successfully updated.');
          this.auctionEditForm.reset();
          this.bsModalRef.hide();
        } else {
          this.notificationService.error('Could not update auction.');
        }
      }, (error: string) => this.notificationService.error(error));
  }
}
