import { Component, OnInit, Input } from "@angular/core";

@Component({
  selector: "app-auction-creator",
  templateUrl: "./auction-creator.component.html",
  styleUrls: ["./auction-creator.component.scss"]
})
export class AuctionCreatorComponent implements OnInit {
  @Input() creatorDetails: Auctions.AuctionCreatorDetailsModel;
  @Input() isLoggedIn: boolean;

  constructor() {}

  ngOnInit() {}
}
