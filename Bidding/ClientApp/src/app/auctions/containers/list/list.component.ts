// angular
import { Component, OnInit } from "@angular/core";

// 3rd lib
import { Subscription } from "rxjs";

// internal
import { AuctionsService } from "../../services/auctions.service";
import { NotificationsService } from "ClientApp/src/app/core";
import { AuctionListRequestModel } from "../../models/list/auction-list-request.model";
import { AuctionListResponseModel } from "../../models/list/auction-list-response.model";

@Component({
  selector: "app-auction-list",
  templateUrl: "./list.component.html"
})
export class AuctionListComponent implements OnInit {
  listSubscription: Subscription;

  // API
  auctionTable: AuctionListResponseModel;
  auctionListRequest: AuctionListRequestModel;

  // pagination || form
  numberRows = 15;
  currentPage = 1;
  isLoading = true;
  selected: [] = [];

  constructor(
    private auctionService: AuctionsService,
    private notificationService: NotificationsService
  ) {}

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
      offsetStart: 0,
      offsetEnd: this.numberRows,
      currentPage: this.currentPage,
      sortByColumn: "AuctionName", // by default sort by auction name
      sortingDirection: "asc", // by default ascending
      searchValue: ""
    };
  }

  /** Gets only active auctions */
  private loadActiveAuctions(): void {
    this.listSubscription = this.auctionService
      .getAuctions$(this.auctionListRequest)
      .subscribe(
        (response: AuctionListResponseModel) => {
          this.auctionTable = response;
          this.isLoading = false;
        },
        (error: string) => this.notificationService.error(error)
      );
  }
}
