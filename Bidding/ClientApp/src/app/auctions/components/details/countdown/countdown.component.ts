// angular
import { Component, OnInit, OnDestroy, Input } from "@angular/core";

// 3rd lib
import { Subscription, interval } from "rxjs";
import { map } from "rxjs/operators";
import * as moment from "moment-mini";

@Component({
  selector: "app-auction-details-countdown",
  templateUrl: "./countdown.component.html",
  styleUrls: ["./countdown.component.scss"]
})
export class AuctionDetailsCountdownComponent implements OnInit, OnDestroy {
  @Input() aboutDetails: Auctions.AboutAuctionDetailsModel;

  /** Auction details countdown component subscription */
  countdownSub: Subscription;

  // countdown time params
  difference: number;
  weeks: number = 0;
  days: number = 0;
  hours: number = 0;
  minutes: number = 0;

  // countdown text
  countdownText: string = "Līdz izsolei palicis";

  intervallTimer = interval(1000);
  private alive = true;

  constructor() {}

  ngOnInit(): void {
    this.countdownSub = this.intervallTimer
      .pipe(
        map(x => (this.difference = this.handleDates()))
        // takeWhile(() => !this.alive)
      )
      .subscribe(() => this.setTime());
  }

  ngOnDestroy(): void {
    if (this.countdownSub) {
      this.countdownSub.unsubscribe();
    }
  }

  private handleDates(): number {
    let applyDateDiff = this.timeDifferenceInSeconds(
      this.aboutDetails.auctionApplyTillDate
    );

    if (applyDateDiff <= 0) {
      let startDateDiff = this.timeDifferenceInSeconds(
        this.aboutDetails.auctionStartDate
      );

      if (startDateDiff <= 0) {
        let endDateDiff = this.timeDifferenceInSeconds(
          this.aboutDetails.auctionEndDate
        );

        if (endDateDiff <= 0) {
          this.countdownText = "Izsole beigusies";
          this.alive = false; // I just do this so I know I've cleared the interval
        } else {
          this.countdownText = "Izsole beigsies pēc";
          return endDateDiff;
        }
      } else {
        this.countdownText = "Izsole notiek tagad";
        return startDateDiff;
      }
    } else {
      this.countdownText = "Līdz izsolei palicis";
      return applyDateDiff;
    }
  }

  private timeDifferenceInSeconds(date: Date): number {
    return moment(date).diff(moment(), "seconds", true);
  }

  private setTime(): void {
    this.weeks = this.getWeeks(this.difference);

    if (this.weeks <= 0) {
      this.weeks = 0;
      this.days = this.getDays(this.difference);
      this.hours = this.getHours(this.difference);
      this.minutes = this.getMinutes(this.difference);
    } else {
      this.days = this.getDays(this.difference - this.weeks * 604800);
      this.hours = this.getHours(this.difference);
      this.minutes = this.getMinutes(this.difference);
    }
  }

  private getWeeks(time: number): number {
    return Math.floor(time / (60 * 60 * 24 * 7));
  }

  private getDays(time: number): number {
    return Math.floor(time / (60 * 60 * 24));
  }

  private getHours(time: number): number {
    return Math.floor((time / (60 * 60)) % 24);
  }

  private getMinutes(time: number): number {
    return Math.floor((time / 60) % 60);
  }
}
