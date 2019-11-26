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

  difference: number;
  weeks: number = 0;
  days: number = 0;
  hours: number = 0;
  minutes: number = 0;

  private skipApplyDate: boolean;

  constructor() {}

  ngOnInit(): void {
    // this.countdownSub = interval(1000)
    //   .pipe(map(x => this.handleDates()))
    //   .subscribe(x => this.setTime());
  }

  ngOnDestroy(): void {
    if (this.countdownSub) {
      this.countdownSub.unsubscribe();
    }
  }

  private handleDates() {
    // TODO: KKE: ADD CHECKS HERE FOR ALL DATES AND HANDLE ALL OF THEM IF THEY ARE endec & run out!
    let applyDateDiff: number;

    if (applyDateDiff != 0) {
      applyDateDiff = this.timeDifferenceInSeconds(this.aboutDetails.auctionApplyTillDate);
      return;
    }

    if (applyDateDiff == 0) {
      this.skipApplyDate = true;
      let startDateDiff = this.timeDifferenceInSeconds(this.aboutDetails.auctionStartDate);
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
