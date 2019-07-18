// angular
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

// 3rd party
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';

// internal
import { AuctionsService } from '../../services/auctions.service';
import { FormService } from 'ClientApp/src/app/core/services/form/form.service';
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';
import { AuctionEditRequestModel } from '../../models/edit/auction-edit-request.model';
import { AuctionEditDetailsResponseModel } from '../../models/edit/auction-edit-details-response.model';


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
    auctionApplyTillDate: '',
    auctionEndDate: '',
    auctionStatus: ''
  };

  // used in the template to show || hide the loading spinner
  loaded = false;

  /** Edit request model */
  auctionEditRequest: AuctionEditRequestModel;

  /** Specific auction details response model */
  auctionDetails: AuctionEditDetailsResponseModel;

  /** Convenience getter for easy access to form fields */
  get f() { return this.auctionEditForm.controls; }

  constructor(
    private auctionService: AuctionsService,
    private notificationService: NotificationsService,
    private formBuilder: FormBuilder,
    private formService: FormService,
    public bsModalRef: BsModalRef,
    private externalModalService: BsModalService
  ) { }

  ngOnInit(): void {
    this.loadAuctionDetails();
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

  private loadAuctionDetails(): void {
    this.auctionEditSubscription = this.auctionService
      .getAuctionEditDetails$(this.selectedAuctionId)
      .subscribe(
        (editDetails: AuctionEditDetailsResponseModel) => {
          this.loaded = true;
          this.auctionDetails = editDetails;
          this.buildForm();
        },
        (error: string) => this.notificationService.error(error)
      );
  }

  private buildForm(): void {
    this.auctionEditForm = this.formBuilder.group({
      auctionName: [this.auctionDetails.auction.auctionName, [Validators.required]],
      auctionStartingPrice: [this.auctionDetails.auction.auctionStartingPrice, []],
      auctionStartDate: [this.auctionDetails.auction.auctionStartDate, []],
      auctionApplyTillDate: [this.auctionDetails.auction.auctionApplyTillDate, [Validators.required]],
      auctionEndDate: [this.auctionDetails.auction.auctionEndDate, [Validators.required]],
      auctionStatus: [this.auctionDetails.auction.auctionStatusId, [Validators.required]] // todo: kke: needs to be select!
    });

    this.auctionEditForm.valueChanges.subscribe((data) => {
      this.formErrors = this.formService.validateForm(this.auctionEditForm, this.formErrors, true);
    });
  }

  private makeUpdateRequest(): void {
    this.setUpdateRequestValues();

    this.auctionService.editAuction$(this.auctionEditRequest)
      .subscribe((editSuccess: boolean) => {
        if (editSuccess) {
          this.notificationService.success('Auction successfully updated.');
          this.auctionEditForm.reset();
          this.bsModalRef.hide();
          this.externalModalService.setDismissReason('Update');
        } else {
          this.notificationService.error('Could not update auction.');
        }
      }, (error: string) => this.notificationService.error(error));
  }

  private setUpdateRequestValues(): void {
    this.auctionEditRequest = {
      auctionId: this.selectedAuctionId,
      auctionName: this.auctionEditForm.value.auctionName,
      auctionStartingPrice: this.auctionEditForm.value.auctionStartingPrice,
      auctionStartDate: this.auctionEditForm.value.auctionStartDate,
      auctionApplyTillDate: this.auctionEditForm.value.auctionApplyTillDate,
      auctionEndDate: this.auctionEditForm.value.auctionEndDate,
      auctionStatusId: 1 // this.auctionEditForm.value.auctionStatus
    };
  }
}
