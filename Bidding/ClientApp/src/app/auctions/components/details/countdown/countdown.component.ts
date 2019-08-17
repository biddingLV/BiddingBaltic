// angular
import { Component, OnInit, OnDestroy, Input } from "@angular/core";

// 3rd lib
import { Subscription, interval } from "rxjs";
import * as moment from "moment-mini";

// internal
import { AuctionDetailsModel } from "../../../models/details/auction-details.model";

@Component({
  selector: "app-auction-details-countdown",
  templateUrl: "./countdown.component.html"
})
export class AuctionDetailsCountdownComponent implements OnInit, OnDestroy {
  @Input() auctionDetails: AuctionDetailsModel;

  /** Auction details countdown component subscription */
  countdownSub: Subscription;

  countdown = {
    days: 0,
    hours: 0,
    minutes: 0
  };

  constructor() {}

  ngOnInit(): void {
    // this.countdownSub = interval(1000).subscribe(value => this.setupCountdown());
  }

  ngOnDestroy(): void {
    if (this.countdownSub) {
      this.countdownSub.unsubscribe();
    }
  }

  private setupCountdown(): void {
    const eventTime = moment(
      "10-06-2019 10:00:00",
      "DD-MM-YYYY HH:mm:ss"
    ).unix();
    const currentTime = moment().unix();
    const diffTime = eventTime - currentTime;
    let duration = moment.duration(diffTime * 1000, "milliseconds");
    const interval = 1000;

    duration = moment.duration(
      duration.asMilliseconds() - interval,
      "milliseconds"
    );

    if (diffTime > 0) {
      this.countdown.days = moment.duration(duration).days();
      this.countdown.hours = moment.duration(duration).hours();
      this.countdown.minutes = moment.duration(duration).minutes();
    }
  }
}
