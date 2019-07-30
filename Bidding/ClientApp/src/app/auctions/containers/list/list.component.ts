// angular
import { Component, OnInit } from '@angular/core';

// 3rd lib
import { Subscription } from 'rxjs';
import * as moment from 'moment-mini';

// internal
import { AuctionsService } from '../../services/auctions.service';
import { NotificationsService } from 'ClientApp/src/app/core';
import { AuctionModel } from '../../models/list/auction.model';
import { AuctionListRequest } from '../../models/list/auction-list-request.model';


@Component({
  selector: 'app-auction-list',
  templateUrl: './list.component.html'
})
export class AuctionListComponent implements OnInit {
  listSubscription: Subscription;

  // API
  auctionTable: AuctionModel;
  auctionListRequest: AuctionListRequest;

  // pagination || form
  numberRows = 15;
  currentPage = 1;
  isLoading = true;
  selected: [] = [];

  constructor(
    private auctionService: AuctionsService,
    private notificationService: NotificationsService
  ) { }

  ngOnInit(): void {
    this.setupInitialAuctionRequest();
    this.loadActiveAuctions();
  }

  private updateColumns(page: number): void {
    this.auctionListRequest.offsetStart = page;

    this.loadActiveAuctions();
  }

  private setupInitialAuctionRequest(): void {
    this.auctionListRequest = {
      auctionStartDate: moment().subtract(365, 'days').format('DD/MM/YYYY'),
      auctionEndDate: moment().format('DD/MM/YYYY'),
      offsetStart: 0,
      offsetEnd: this.numberRows,
      currentPage: this.currentPage,
      sortByColumn: 'AuctionName', // by default sort by auction name
      sortingDirection: 'asc', // by default ascending
      searchValue: ''
    };
  }

  /** Gets only active auctions */
  private loadActiveAuctions(): void {
    this.listSubscription = this.auctionService
      .getAuctions$(this.auctionListRequest)
      .subscribe(
        (response: AuctionModel) => {
          this.auctionTable = response;
          this.isLoading = false;
        },
        (error: string) => this.notificationService.error(error)
      );
  }
}
