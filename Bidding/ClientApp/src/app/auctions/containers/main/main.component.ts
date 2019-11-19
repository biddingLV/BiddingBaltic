// angular
import { Component, OnInit } from "@angular/core";

// 3rd lib
import { Subscription } from "rxjs";
import { startWith } from "rxjs/operators";

// internal
import { AuctionsService } from "../../services/auctions.service";
import { NotificationsService } from "ClientApp/src/app/core/services/notifications/notifications.service";
import { AuctionFilterModel } from "../../models/filters/auction-filter.model";
import { SubCategoryFilterModel } from "../../models/filters/sub-category-filter.model";
import { AuctionListResponseModel } from "../../models/list/auction-list-response.model";
import { AuctionListRequestModel } from "../../models/list/auction-list-request.model";

@Component({
  selector: "app-auction-main",
  templateUrl: "./main.component.html",
  styleUrls: ["./main.component.scss"]
})
export class AuctionMainComponent implements OnInit {
  // component
  mainSubscription: Subscription;

  // used to pass selected filter values to the auction list component
  selectedCategoryIds: number[];
  selectedTypeIds: number[];
  specifiedSearchText: string;

  selected?: any[] = [];

  // API
  auctionTable: AuctionListResponseModel;
  auctionListRequest: AuctionListRequestModel;

  // pagination || form
  numberRows = 15;
  currentPage = 1;

  // filters
  filters: AuctionFilterModel;
  auctionTypes: SubCategoryFilterModel[];

  constructor(
    private auctionService: AuctionsService,
    private notificationService: NotificationsService
  ) {}

  ngOnInit(): void {
    this.loadFilters();
  }

  /** Called on auction search event */
  onSearch(searchText: string): void {
    this.specifiedSearchText = searchText;
  }

  // Request Update Events
  updateRequest(property: string, event): void {
    if (property === "Page") {
      this.auctionListRequest.currentPage = event.page;
    } else {
      this.auctionListRequest.searchValue = event;
      this.auctionListRequest.currentPage = 1;
    }
  }

  // Sort Update Events
  onSortChange(event): void {
    this.auctionListRequest.sortingDirection =
      this.auctionListRequest.sortByColumn === event.column.prop
        ? this.auctionListRequest.sortingDirection === "asc"
          ? "desc"
          : "asc"
        : "asc";

    this.auctionListRequest.sortByColumn = event.column.prop;
    this.auctionListRequest.currentPage = 1;
  }

  // todo: kke: is this even needed here?
  onSelectedChange(event): void {
    // this.selectedChange.emit(event);
  }

  onDetailsClick(): void {}

  onTopCategoryChange(categoryIds: number[]) {
    this.selectedCategoryIds = categoryIds;
  }

  onSubCategoryChange(typeIds: number[]) {
    this.selectedTypeIds = typeIds;
  }

  /** Load top & sub categories */
  private loadFilters(): void {
    this.mainSubscription = this.auctionService
      .getFilters$()
      .pipe(startWith(new AuctionFilterModel()))
      .subscribe(
        (response: AuctionFilterModel) => {
          this.filters = response;
          this.auctionTypes = response.subCategories;
        },
        (error: string) => this.notificationService.error(error)
      );
  }
}
