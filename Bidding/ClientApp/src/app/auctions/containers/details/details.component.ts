// angular
import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

// rxjs
import { Subscription } from 'rxjs';
import { switchMap } from 'rxjs/operators';

// internal
import { AuctionsService } from '../../services/auctions.service';
import { AuctionDetailsModel } from '../../models/details/auction-details.model';
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';
import { AuctionListRequest } from '../../models/list/auction-list-request.model';


@Component({
  selector: 'app-auction-details',
  templateUrl: './details.component.html',
  styleUrls: []
})
export class AuctionDetailsComponent implements OnInit, OnDestroy {
  // details
  auctionDetailsSub: Subscription;
  auctionDetails: AuctionDetailsModel;

  // utility
  loading: boolean;

  // API
  request: AuctionListRequest;

  timer: any;

  displayTime: any;

  constructor(
    private auctionApi: AuctionsService,
    private notification: NotificationsService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.getAuctionDetails();

    this.timer = setInterval(() => {
      // github.com/markpenaranda/ngx-countdown-timer/blob/master/src/countdown-timer.component.ts
      // if (this.start) {
      this.displayTime = this.getTimeDifference('3/4/2019 11:15');
      // } else {
      //   this.displayTime = this.getTimeDifference(this.end);
      // }
    }, 1000);
  }

  ngOnDestroy(): void {
    if (this.auctionDetailsSub) {
      this.auctionDetailsSub.unsubscribe();
    }
  }

  getTimeDifference(datetime) {
    datetime = new Date(datetime).getTime();
    const now = new Date().getTime();

    if (isNaN(datetime)) {
      return '';
    }

    const milisec_diff = datetime - now;

    const days = Math.floor(milisec_diff / 1000 / 60 / (60 * 24));
    const date_diff = new Date(milisec_diff);
    const day_string = (days) ? this.twoDigit(days) + ':' : '';
    const day_hours = days * 24;


    return day_string + this.twoDigit(date_diff.getUTCHours()) +
      ':' + this.twoDigit(date_diff.getMinutes()) + ':'
      + this.twoDigit(date_diff.getSeconds());

  }

  private getAuctionDetails(): void {
    this.auctionDetailsSub =
      this.route.paramMap.pipe(
        switchMap((params: ParamMap) =>
          this.auctionApi.getAuctionDetails$(Number(params.get('id'))))
      ).subscribe(response => { this.auctionDetails = response; },
        (error: string) => this.notification.error(error));
  }

  private twoDigit(number: number) {
    return number > 9 ? '' + number : '0' + number;
  }
}
