// angular
import { Component, OnInit, Input, SimpleChanges } from "@angular/core";
import { ActivatedRoute, ParamMap } from "@angular/router";

// 3rd lib
import { Subscription } from "rxjs";

// internal
import { AuctionsService } from "../../services/auctions.service";
import { AuctionListRequestModel } from "../../models/list/auction-list-request.model";
import { AuctionListResponseModel } from "../../models/list/auction-list-response.model";
import { CategoryConstants } from "ClientApp/src/app/core/constants/categories/category-constants";
import { NotificationsService } from "ClientApp/src/app/core/services/notifications/notifications.service";
import { AuthService } from "ClientApp/src/app/core/services/auth/auth.service";

@Component({
  selector: "app-auction-list",
  templateUrl: "./list.component.html"
})
export class AuctionListComponent implements OnInit {
  @Input() selectedCategoryIds: number[];
  @Input() selectedTypeIds: number[];
  @Input() specifiedSearchText: string;

  listSubscription: Subscription;

  // API
  auctionTable: AuctionListResponseModel;
  auctionListRequest: AuctionListRequestModel;
  loggedInUserId: number;

  // pagination || form
  numberRows = 15;
  currentPage = 1;
  isLoading = true;
  selected: [] = [];

  constructor(
    private auctionService: AuctionsService,
    private notificationService: NotificationsService,
    private activatedRoute: ActivatedRoute,
    private authService: AuthService
  ) {
    if (this.authService.userDetails) {
      this.loggedInUserId = this.authService.userDetails.UserId;
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    const categoryIdsChange = changes["selectedCategoryIds"];
    const typeIdsChange = changes["selectedTypeIds"];
    const searchTextChange = changes["specifiedSearchText"];

    if (categoryIdsChange && !categoryIdsChange.isFirstChange()) {
      this.auctionListRequest.topCategoryIds = categoryIdsChange.currentValue;

      this.loadActiveAuctions();
    }

    if (typeIdsChange && !typeIdsChange.isFirstChange()) {
      this.auctionListRequest.typeIds = typeIdsChange.currentValue;

      this.loadActiveAuctions();
    }

    if (searchTextChange && !searchTextChange.isFirstChange()) {
      this.auctionListRequest.searchValue = searchTextChange.currentValue;

      this.loadActiveAuctions();
    }

    return;
  }

  ngOnInit(): void {
    this.listSubscription = this.activatedRoute.queryParams.subscribe(
      params => {
        let filterParam = params["filtrs"];

        if (filterParam) {
          this.handleCardLinkClick(filterParam);
        } else {
          this.setupInitialAuctionRequest();
          this.loadActiveAuctions();
        }
      }
    );
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

  private handleCardLinkClick(filterParam: any) {
    if (filterParam == CategoryConstants.VEHICLE_CATEGORY_NAME) {
      this.setupCardAuctionRequest();
      this.auctionListRequest.topCategoryIds = [
        CategoryConstants.VEHICLE_CATEGORY_ID
      ];
    } else if (filterParam == CategoryConstants.PROPERTY_CATEGORY_NAME) {
      this.setupCardAuctionRequest();
      this.auctionListRequest.topCategoryIds = [
        CategoryConstants.PROPERTY_CATEGORY_ID
      ];
    } else if (filterParam == CategoryConstants.ITEM_CATEGORY_NAME) {
      this.setupCardAuctionRequest();
      this.auctionListRequest.topCategoryIds = [
        CategoryConstants.ITEM_CATEGORY_ID
      ];
    }

    this.loadActiveAuctions();
  }

  private setupCardAuctionRequest(): void {
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
    if (this.loggedInUserId) {
      this.loadAuctionsWithSearch();
    } else {
      this.loadAuctionsWithoutSearch();
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
