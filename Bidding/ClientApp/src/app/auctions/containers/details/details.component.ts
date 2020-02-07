// angular
import { Component, OnInit, OnDestroy } from "@angular/core";
import { ActivatedRoute, ParamMap } from "@angular/router";

// 3rd lib
import { Subscription } from "rxjs";
import { switchMap } from "rxjs/operators";
import * as moment from "moment-mini";

// internal
import { AuctionsService } from "../../services/auctions.service";
import { AuctionDetailsModel } from "../../models/details/auction-details.model";
import { NotificationsService } from "ClientApp/src/app/core/services/notifications/notifications.service";
import { BreadcrumbItem } from "ClientApp/src/app/shared/models/breadcrumb-item.model";
import { AuthService } from "ClientApp/src/app/core/services/auth/auth.service";
import { ButtonsService } from "ClientApp/src/app/core/services/buttons/buttons.service";
import { CustomButtonModel } from "ClientApp/src/app/core/services/buttons/custom-button.model";

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
  userDetails = this.authService.userDetails;
  isLoggedIn: boolean;
  buttonConfig: CustomButtonModel;

  // breadcrumbs
  breadcrumbs: BreadcrumbItem[];

  constructor(
    private auctionService: AuctionsService,
    private notificationService: NotificationsService,
    private activatedRoute: ActivatedRoute,
    private authService: AuthService,
    private buttonsService: ButtonsService
  ) {}

  ngOnInit(): void {
    this.isLoggedIn = this.userDetails != null ? true : false;
    this.handleSignInButton();
    this.getAuctionDetails();
  }

  ngOnDestroy(): void {
    if (this.auctionDetailsSub) {
      this.auctionDetailsSub.unsubscribe();
    }
  }

  /**
   * Transforms passed date to be date in users local timezone
   * @param activityDate Date in UTC format
   */
  getLocalTime(date: any): string {
    let parsedDate = moment(date).format(this.dateFormat);
    let utcDate = moment.utc(parsedDate).toDate();
    let localDate = moment(utcDate)
      .local()
      .format(this.dateFormat);

    return localDate;
  }

  /** Used to handle sign-in */
  onSignInChange(): void {
    this.authService.login();
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

  private handleSignInButton(): void {
    this.buttonConfig = {
      ...this.buttonsService.defaultButtonConfig,
      ...{ class: "btn-primary btn-lg rounded-pill font-weight-bold" },
      ...{ styles: { cursor: "pointer", fontSize: "16px" } }
    };
  }
}
