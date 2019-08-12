// angular
import { Component, OnInit, OnDestroy } from "@angular/core";
import { ActivatedRoute, ParamMap } from "@angular/router";

// 3rd lib
import { Subscription } from "rxjs";
import { switchMap } from "rxjs/operators";

// internal
import { AuctionsService } from "../../services/auctions.service";
import { AuctionDetailsModel } from "../../models/details/auction-details.model";
import { NotificationsService } from "ClientApp/src/app/core/services/notifications/notifications.service";

@Component({
  selector: "app-auction-details",
  templateUrl: "./details.component.html"
})
export class AuctionDetailsComponent implements OnInit, OnDestroy {
  /** Auction details component subscription */
  auctionDetailsSub: Subscription;

  /** Auction details model object */
  auctionDetails: AuctionDetailsModel;

  dateFormat = "dd/MM/yyyy";

  constructor(
    private auctionApi: AuctionsService,
    private notification: NotificationsService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.getAuctionDetails();
  }

  ngOnDestroy(): void {
    if (this.auctionDetailsSub) {
      this.auctionDetailsSub.unsubscribe();
    }
  }

  private getAuctionDetails(): void {
    this.auctionDetailsSub = this.route.paramMap
      .pipe(
        switchMap((params: ParamMap) =>
          this.auctionApi.getAuctionDetails$(Number(params.get("id")))
        )
      )
      .subscribe(
        response => {
          this.auctionDetails = response;
        },
        (error: string) => this.notification.error(error)
      );
  }
}
