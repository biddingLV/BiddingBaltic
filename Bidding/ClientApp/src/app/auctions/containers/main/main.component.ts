// angular
import { Component, OnInit, OnDestroy } from "@angular/core";

// internal
import { AuctionListResponseModel } from "../../models/list/auction-list-response.model";
import { AuctionListRequestModel } from "../../models/list/auction-list-request.model";
import { BreadcrumbItem } from "ClientApp/src/app/shared/models/breadcrumb-item.model";

@Component({
  selector: "app-auction-main",
  templateUrl: "./main.component.html",
  styleUrls: ["./main.component.scss"],
})
export class AuctionMainComponent implements OnInit {
  // component
  specifiedSearchText: string;
  selected?: any[] = [];

  // API
  auctionTable: AuctionListResponseModel;
  auctionListRequest: AuctionListRequestModel;

  // pagination || form
  numberRows = 15;
  currentPage = 1;

  // breadcrumbs
  breadcrumbs: BreadcrumbItem[];

  constructor() {}

  ngOnInit(): void {
    this.generateBreadcrumbs();
  }

  /** Called on auction search event */
  onSearch(searchText: string): void {
    this.specifiedSearchText = searchText;
  }

  private generateBreadcrumbs() {
    this.breadcrumbs = [
      {
        name: "Sākumlapa",
        url: "/",
      },
      {
        name: "Izsoles",
        url: "/izsoles",
      },
    ];
  }
}
