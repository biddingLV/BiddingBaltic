// angular
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

// 3rd party
import { BsModalRef } from 'ngx-bootstrap/modal';

// internal
import { AuctionsService } from '../../services/auctions.service';
import { FormService } from 'ClientApp/src/app/core/services/form/form.service';
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';
import { AuctionAdd2Request } from '../../models/add/auction-add2-request.model';


@Component({
  selector: 'app-auction-add2',
  templateUrl: './add.component.html'
})
export class AuctionAdd2Component implements OnInit {
  // API
  auctionAddRequest: AuctionAdd2Request;
  // form
  auctionAddForm: FormGroup;

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
    this.makeRequest();
  }

  private buildForm(): void {
    this.auctionAddForm = this.fb.group({
      auctionName: ['', []]
    });
  }

  private setAddRequest(): void {
    this.auctionAddRequest = {
      auctionName: this.auctionAddForm.value.auctionName
    };
  }

  private makeRequest(): void {
    this.setAddRequest();

    this.auctionService.addAuction$(this.auctionAddRequest)
      .subscribe((data: boolean) => {
        let editSuccess = data;
        if (editSuccess) {
          this.notificationService.success('Auction successfully added.');
          this.auctionAddForm.reset();
          this.bsModalRef.hide();
        } else {
          this.notificationService.error('Could not added auction.');
        }
      },
        (error: string) => this.notificationService.error(error));
  }
}
