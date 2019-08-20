// angular
import { Component, OnInit, Input, SimpleChanges } from "@angular/core";

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
  @Input() selectedCategoryIds: number[];
  @Input() selectedTypeIds: number[];
  @Input() disableSearch: boolean;

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

  ngOnChanges(changes: SimpleChanges): void {
    const categoryIdsChange = changes["selectedCategoryIds"];
    const typeIdsChange = changes["selectedTypeIds"];

    if (categoryIdsChange && !categoryIdsChange.isFirstChange()) {
      this.auctionListRequest.topCategoryIds = categoryIdsChange.currentValue;
    } else if (typeIdsChange && !typeIdsChange.isFirstChange()) {
      this.auctionListRequest.typeIds = typeIdsChange.currentValue;
    } else {
      return;
    }

    this.loadActiveAuctions();
  }

  ngOnInit(): void {
    this.setupInitialAuctionRequest();
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
    if (this.disableSearch) {
      this.loadAuctionsWithoutSearch();
    } else {
      this.loadAuctionsWithSearch();
    }
  }

  private loadAuctionsWithSearch() {
    this.listSubscription = this.auctionService
      .getAuctionsWithSearch$(this.auctionListRequest)
      .subscribe(
        (response: AuctionListResponseModel) => {
          this.auctionTable = response;
          this.isLoading = false;
        },
        (error: string) => this.notificationService.error(error)
      );
  }

  private loadAuctionsWithoutSearch() {
    this.listSubscription = this.auctionService
      .getAuctionsWithoutSearch$(this.auctionListRequest)
      .subscribe(
        (response: AuctionListResponseModel) => {
          this.auctionTable = response;
          this.isLoading = false;
        },
        (error: string) => this.notificationService.error(error)
      );
  }
}
