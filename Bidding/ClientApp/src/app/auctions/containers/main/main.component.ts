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
import { AuctionModel } from "../../models/list/auction.model";
import { AuctionListRequest } from "../../models/list/auction-list-request.model";

@Component({
  selector: "app-auction-main",
  templateUrl: "./main.component.html"
})
export class AuctionMainComponent implements OnInit {
  mainSubscription: Subscription;

  selected?: any[] = [];

  // API
  auctionTable: AuctionModel;
  auctionListRequest: AuctionListRequest;

  // pagination || form
  numberRows = 15;
  currentPage = 1;

  constructor() {}

  ngOnInit(): void {}

  /** Called on auction search event */
  onSearch(text: string): void {
    if (text !== undefined) {
      this.auctionListRequest.searchValue = text;
    } else {
      this.auctionListRequest.searchValue = "";
    }

    // this.updateColumns(1);
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
}
