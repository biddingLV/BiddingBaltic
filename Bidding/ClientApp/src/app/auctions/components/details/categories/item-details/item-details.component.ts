// angular
import { Component, OnInit, Input } from "@angular/core";

@Component({
  selector: "app-auction-details-item-category",
  templateUrl: "./item-details.component.html",
  styleUrls: ["./item-details.component.scss"]
})
export class ItemDetailsComponent implements OnInit {
  @Input() details: Auctions.ItemAuctionDetailsModel;

  constructor() {}

  ngOnInit() {}
}
