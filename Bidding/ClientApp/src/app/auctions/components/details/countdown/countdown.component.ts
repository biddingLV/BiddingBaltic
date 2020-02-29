// angular
import { Component, OnInit, OnDestroy, Input } from "@angular/core";

// 3rd lib
import { interval } from "rxjs";
import { map, takeWhile } from "rxjs/operators";
import * as moment from "moment-mini";

// internal
import { AboutAuctionDetailsModel } from "../../../models/details/about-auction-details.model";
import { CountdownOptionsModel } from "../../../models/details/countdown-options.model";

@Component({
  selector: "app-auction-details-countdown",
  templateUrl: "./countdown.component.html",
  styleUrls: ["./countdown.component.scss"]
})
export class AuctionDetailsCountdownComponent implements OnInit, OnDestroy {
  @Input() aboutDetails: AboutAuctionDetailsModel;

  options: CountdownOptionsModel = {
    countdownText: "Līdz izsolei palicis",
    difference: 0,
    weeks: 0,
    days: 0,
    hours: 0,
    minutes: 0
  };

  private currentTimestamp: number;
  private intervalTimer = interval(1000);
  private auctionAlive: boolean = true;

  constructor() {}

  ngOnInit(): void {
    this.setupTimer();
  }

  ngOnDestroy(): void {}

  private setupTimer(): void {
    this.intervalTimer
      .pipe(
        map(item => {
          this.options.difference = this.handleDates();
        }),
        takeWhile(val => this.auctionAlive)
      )
      .subscribe(time => {
        this.options.weeks = this.getWeeks(this.options.difference);
        this.options.days = this.getDays(this.options.difference);
        this.options.hours = this.getHours(this.options.difference);
        this.options.minutes = this.getMinutes(this.options.difference);
      });
  }

  private handleDates(): number {
    this.currentTimestamp = moment().valueOf();
    this.auctionAlive = true;

    const applyDateTimestamp = this.convertToPrimitiveValue(
      this.aboutDetails.auctionApplyTillDate
    );

    if (applyDateTimestamp > this.currentTimestamp) {
      return this.handleApplyDate();
    }

    if (this.aboutDetails.auctionStartDate != undefined) {
      // Note: KKE: Start date is OPTIONAL!
      const startDateTimestamp = this.convertToPrimitiveValue(
        this.aboutDetails.auctionStartDate
      );

      if (startDateTimestamp > this.currentTimestamp) {
        return this.handleStartDate();
      }
    }

    const endDateTimestamp = this.convertToPrimitiveValue(
      this.aboutDetails.auctionEndDate
    );

    if (endDateTimestamp > this.currentTimestamp) {
      return this.handleEndDate();
    }

    this.handleAuctionEnded();
  }

  private handleAuctionEnded(): void {
    this.options.countdownText = "Izsole beigusies";
    this.options.weeks = 0;
    this.options.days = 0;
    this.options.hours = 0;
    this.options.minutes = 0;
    this.auctionAlive = false;
  }

  private convertToPrimitiveValue(date: Date): number {
    return moment(date).valueOf();
  }

  private handleApplyDate(): number {
    this.options.countdownText = "Līdz izsolei palicis";
    return this.timeDifferenceInSeconds(this.aboutDetails.auctionApplyTillDate);
  }

  private handleStartDate(): number {
    this.options.countdownText = "Izsole notiek tagad";
    return this.timeDifferenceInSeconds(this.aboutDetails.auctionStartDate);
  }

  private handleEndDate(): number {
    this.options.countdownText = "Izsole beigsies pēc";
    return this.timeDifferenceInSeconds(this.aboutDetails.auctionEndDate);
  }

  private timeDifferenceInSeconds(date: Date): number {
    return moment(date).diff(moment(), "seconds", true);
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
