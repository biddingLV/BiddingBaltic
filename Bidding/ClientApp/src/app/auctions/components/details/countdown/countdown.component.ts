// angular
import { Component, OnInit, OnDestroy, Input } from "@angular/core";

// 3rd lib
import { Subscription, interval } from "rxjs";
import { map } from "rxjs/operators";

// internal
import { AuctionDetailsModel } from "../../../models/details/auction-details.model";

@Component({
  selector: "app-auction-details-countdown",
  templateUrl: "./countdown.component.html",
  styleUrls: ["./countdown.component.scss"]
})
export class AuctionDetailsCountdownComponent implements OnInit, OnDestroy {
  @Input() auctionDetails: AuctionDetailsModel;

  /** Auction details countdown component subscription */
  countdownSub: Subscription;

  enddate = "2019-11-29";

  private difference: number;
  private weeks: number = 0;
  private days: number = 0;
  private hours: number = 0;
  private minutes: number = 0;
  private seconds: number = 0;

  constructor() {}

  ngOnInit(): void {
    this.countdownSub = interval(1000)
      .pipe(map(x => this.getTimeDifference()))
      .subscribe(x => this.setTime());
  }

  ngOnDestroy(): void {
    if (this.countdownSub) {
      this.countdownSub.unsubscribe();
    }
  }

  private getTimeDifference(): void {
    this.difference =
      Date.parse(this.enddate) - Date.parse(new Date().toString());
  }

  private setTime(): void {
    this.weeks = this.getWeeks(this.difference);

    if (this.weeks <= 0) {
      this.weeks = 0;
      this.days = this.getDays(this.difference);
      this.hours = this.getHours(this.difference);
      this.minutes = this.getMinutes(this.difference);
      this.seconds = this.getSeconds(this.difference);
    } else {
      var magic = this.difference - this.weeks * 604800000;

      this.days = this.getDays(magic);
      this.hours = this.getHours(this.difference);
      this.minutes = this.getMinutes(this.difference);
      this.seconds = this.getSeconds(this.difference);
    }
  }

  private getWeeks(time: number): number {
    return Math.floor(time / (1000 * 60 * 60 * 24 * 7));
  }

  private getDays(time: number): number {
    return Math.floor(time / (1000 * 60 * 60 * 24));
  }

  private getHours(time: number): number {
    return Math.floor((time / (1000 * 60 * 60)) % 24);
  }

  private getMinutes(time: number): number {
    return Math.floor((time / 1000 / 60) % 60);
  }

  private getSeconds(time: number): number {
    return Math.floor((time / 1000) % 60);
  }
}
