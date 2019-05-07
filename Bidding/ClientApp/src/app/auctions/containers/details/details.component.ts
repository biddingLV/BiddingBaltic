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

import { Config } from 'ngx-countdown';

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

  //CountDown clock

  timer: any;
  displayTime: any;
  config: Config = {
    template: `$!w!:$!d!:$!h!:$!m!`,
    // need to find a fix
    leftTime: 100 * 100 * 100 * 20,
    // leftTime: Math.floor((this.auctionDetails.auctionEndDate.getTime() - new Date().getTime()) / 1000),
    clock : ['w', 100, 2, 'd', 6, 1, 'h', 24, 2, 'm', 60, 2, 's', 60, 2, 'u', 10, 1]
  }

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
      this.displayTime = this.getTimeDifference(this.auctionDetails.auctionEndDate);
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
    const milisec_diff = datetime - now;
    const countDownSeconds = Math.floor(milisec_diff / 1000);
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
