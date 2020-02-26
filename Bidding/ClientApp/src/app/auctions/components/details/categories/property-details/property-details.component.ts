// angular
import { Component, OnInit, Input } from "@angular/core";

// internal
import { PropertyAuctionDetailsModel } from "ClientApp/src/app/auctions/models/details/property-auction-details.model";

@Component({
  selector: "app-auction-details-property-category",
  templateUrl: "./property-details.component.html",
  styleUrls: ["./property-details.component.scss"]
})
export class PropertyDetailsComponent implements OnInit {
  @Input() details: PropertyAuctionDetailsModel;

  constructor() {}

  ngOnInit() {}
}
