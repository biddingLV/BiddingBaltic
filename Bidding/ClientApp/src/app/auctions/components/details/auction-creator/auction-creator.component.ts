import { Component, OnInit, Input } from "@angular/core";

// internal
import { AuctionCreatorDetailsModel } from "../../../models/details/auction-creator-details.model";

@Component({
  selector: "app-auction-creator",
  templateUrl: "./auction-creator.component.html",
  styleUrls: ["./auction-creator.component.scss"]
})
export class AuctionCreatorComponent implements OnInit {
  @Input() creatorDetails: AuctionCreatorDetailsModel;
  @Input() isLoggedIn: boolean;

  constructor() {}

  ngOnInit() {}
}
