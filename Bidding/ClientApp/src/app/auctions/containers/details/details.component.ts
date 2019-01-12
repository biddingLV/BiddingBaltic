// angular
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

// rxjs
import { Subscription } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { AuctionListRequest } from '../../models/list/auction-list-request.model';

// internal
import { AuctionsService } from '../../services/auctions.service';
import { AuctionDetailsModel } from '../../models/details/auction-details.model';
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';

@Component({
  selector: 'auction-details',
  templateUrl: './details.component.html',
  styleUrls: []
})
export class AuctionDetailsComponent implements OnInit, OnDestroy {
  // details
  auctionDetailsSub: Subscription;
  auctionDetails: AuctionDetailsModel;

  // utility
  loading: boolean;

  // API
  request: AuctionListRequest;

  constructor(
    private auctionApi: AuctionsService,
    private notification: NotificationsService,
    private route: ActivatedRoute
  ) {

  }

  ngOnInit() {
    this.getAuctionDetails();
  }

  ngOnDestroy() {
    if (this.auctionDetailsSub) {
      this.auctionDetailsSub.unsubscribe();
    }
  }

  private getAuctionDetails() {
    this.auctionDetailsSub =
      this.route.paramMap.pipe(
        switchMap((params: ParamMap) =>
          this.auctionApi.getAuctionDetails$(params.get('id')))
      ).subscribe(result => { this.auctionDetails = result; },
        (error: string) => this.notification.error(error));
  }
}
