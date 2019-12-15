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
import { BreadcrumbItem } from "ClientApp/src/app/shared/models/breadcrumb-item.model";

@Component({
  selector: "app-auction-details",
  templateUrl: "./details.component.html",
  styleUrls: ["./details.component.scss"]
})
export class AuctionDetailsComponent implements OnInit, OnDestroy {
  // component
  auctionDetailsSub: Subscription;
  auctionDetails: AuctionDetailsModel;

  // template
  dateFormat = "dd/MM/yyyy HH:mm";

  // breadcrumbs
  breadcrumbs: BreadcrumbItem[];

  constructor(
    private auctionService: AuctionsService,
    private notificationService: NotificationsService,
    private activatedRoute: ActivatedRoute
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
    this.auctionDetailsSub = this.activatedRoute.paramMap
      .pipe(
        switchMap((params: ParamMap) =>
          this.auctionService.getAuctionDetails$(Number(params.get("id")))
        )
      )
      .subscribe(
        response => {
          this.auctionDetails = response;
          this.generateBreadcrumbs();
        },
        (error: string) => this.notificationService.error(error)
      );
  }

  private generateBreadcrumbs() {
    this.breadcrumbs = [
      {
        name: "Sākumlapa",
        url: "/"
      },
      {
        name: "Izsoles",
        url: "/izsoles"
      },
      {
        name: this.auctionDetails.aboutAuctionDetails.auctionName,
        url: ""
      }
    ];
  }
}
