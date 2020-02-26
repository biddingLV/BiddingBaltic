// angular
import { Component, OnInit, Input } from "@angular/core";
import { VehicleAuctionDetailsModel } from "ClientApp/src/app/auctions/models/details/vehicle-auction-details.model";

@Component({
  selector: "app-auction-details-vehicle-category",
  templateUrl: "./vehicle-details.component.html",
  styleUrls: ["./vehicle-details.component.scss"]
})
export class VehicleDetailsComponent implements OnInit {
  @Input() details: VehicleAuctionDetailsModel;

  constructor() {}

  ngOnInit() {}
}
