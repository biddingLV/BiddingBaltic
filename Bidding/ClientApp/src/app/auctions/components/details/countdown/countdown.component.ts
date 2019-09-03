// angular
import { Component, OnInit, OnDestroy, Input } from "@angular/core";

// 3rd lib
import { Subscription, interval } from "rxjs";
import * as moment from "moment-mini";

// internal
import { AuctionDetailsModel } from "../../../models/details/auction-details.model";
import { map, take, startWith } from "rxjs/operators";

@Component({
  selector: "app-auction-details-countdown",
  templateUrl: "./countdown.component.html",
  styleUrls: ["./countdown.component.scss"]
})
export class AuctionDetailsCountdownComponent implements OnInit, OnDestroy {
  @Input() auctionDetails: AuctionDetailsModel;

  //https://stackblitz.com/edit/rxjs-rajp6s?file=index.ts

  /** Auction details countdown component subscription */
  countdownSub: Subscription;

  countdown = {
    days: 0,
    hours: 0,
    minutes: 0,
    seconds: 0
  };

  constructor() {}

  ngOnInit(): void {
// CHECK THIS - https://stackblitz.com/edit/angular-wb5g7m

    // let x = interval(1000).pipe(
    //   map(i => {
    //     5 - i - 1
    //   }),
    //   take(5),
    //   startWith(5)
    // );

    // console.log("x: ", x);

    //   .take()
    // startWith())

    // .subscribe(value => {
    //   console.log(
    //     "TCL: AuctionDetailsCountdownComponent -> constructor -> value",
    //     value
    //   );
    //   this.setupCountdown();
    // });
  }

  ngOnDestroy(): void {
    if (this.countdownSub) {
      this.countdownSub.unsubscribe();
    }
  }

  private setupCountdown(): void {
    const eventTime = moment(
      "19-08-2019 19:25:00",
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
      this.countdown.seconds = moment.duration(duration).seconds();
    }
  }
}
