// angular
import { Component, OnInit, Input } from "@angular/core";

@Component({
  selector: "app-auction-details-property-category",
  templateUrl: "./property-details.component.html",
  styleUrls: ["./property-details.component.scss"]
})
export class PropertyDetailsComponent implements OnInit {
  @Input() details: Auctions.PropertyAuctionDetailsModel;

  constructor() {}

  ngOnInit() {}
}
