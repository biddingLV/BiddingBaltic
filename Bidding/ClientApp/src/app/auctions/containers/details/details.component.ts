import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

import { AuctionsService } from '../../services/auctions.service';
import { NotificationsService } from 'src/app/core/services/notifications/notifications.service';

// rxjs
import { Subscription } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { AuctionListRequest } from '../../models/list/auction-list-request.model';

@Component({
  selector: 'app-auction-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class AuctionDetailsComponent implements OnInit, OnDestroy {
  // table
  auctionDetailsSub: Subscription;

  // utility
  loading: boolean;

  // API
  request: AuctionListRequest;

  constructor(
    private auctionApi: AuctionsService,
    private notification: NotificationsService,
    private route: ActivatedRoute,
    private router: Router
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
    this.route.paramMap.pipe(
      switchMap((params: ParamMap) =>
        this.auctionApi.getAuctionDetails$(params.get('id')))
    );
    // this.loading = true;

    // Get all (admin) events
    // this.auctionsSub = this.auctionApi
    //   .getAuctions$(this.request)
    //   .subscribe(
    //     (result: AuctionModel) => { this.auctionTable = result; },
    //     (error: string) => this.notification.error(error)
    //   );
  }
}
