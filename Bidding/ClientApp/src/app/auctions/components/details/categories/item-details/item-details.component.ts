// angular
import { Component, OnInit, Input } from "@angular/core";

// internal
import { ItemAuctionDetailsModel } from "ClientApp/src/app/auctions/models/details/item-auction-details.model";

@Component({
  selector: "app-auction-details-item-category",
  templateUrl: "./item-details.component.html",
  styleUrls: ["./item-details.component.scss"]
})
export class ItemDetailsComponent implements OnInit {
  @Input() details: ItemAuctionDetailsModel;

  constructor() {}

  ngOnInit() {}
}
