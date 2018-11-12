import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuctionsService } from '../../services/auctions.service';
import { IAuctionListRequest } from '../../models/auction-list-request.model';
import { NotificationsService } from 'src/app/core/services/notifications/notifications.service';

@Component({
  selector: 'app-auction-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class AuctionDetailsComponent implements OnInit, OnDestroy {
  // table
  auctionsSub: Subscription;

  // utility
  loading: boolean;

  // API
  request: IAuctionListRequest;

  constructor(
    private auctionApi: AuctionsService,
    private notification: NotificationsService
  ) { }

  ngOnInit() {
    this.setupRequest();
    this.getAuctionList();
  }

  ngOnDestroy() {
    this.auctionsSub.unsubscribe();
  }

  private setupRequest(): void {
    // this.request = {
    //   starDate: new Date(),
    //   endDate: new Date(),
    //   sizeOfPage: this.numberRows,
    //   currentPage: this.currentPage,
    //   sortByColumn: 'Name',
    //   sortingDirection: 'asc',
    //   searchValue: this.searchValue
    // };
  }

  private getAuctionList() {
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
