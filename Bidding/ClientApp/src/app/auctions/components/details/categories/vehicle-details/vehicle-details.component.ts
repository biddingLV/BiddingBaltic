// angular
import { Component, OnInit, Input } from "@angular/core";

@Component({
  selector: "app-auction-details-vehicle-category",
  templateUrl: "./vehicle-details.component.html",
  styleUrls: ["./vehicle-details.component.scss"]
})
export class VehicleDetailsComponent implements OnInit {
  @Input() details: Auctions.VehicleAuctionDetailsModel;

  constructor() {}

  ngOnInit() {}
}
