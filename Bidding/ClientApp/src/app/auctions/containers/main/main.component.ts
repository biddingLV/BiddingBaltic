// angular
import { Component, OnInit } from '@angular/core';

// 3rd lib
import { Subscription } from 'rxjs';
import { startWith } from 'rxjs/operators';

// internal
import { AuctionsService } from '../../services/auctions.service';
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';
import { AuctionFilterModel } from '../../models/filters/auction-filter.model';


@Component({
  selector: 'app-auction-main',
  templateUrl: './main.component.html',
  styleUrls: []
})
export class AuctionMainComponent implements OnInit {
  // API
  filtersSub: Subscription;

  // filters
  filters: AuctionFilterModel;

  constructor(
    private auctionApi: AuctionsService,
    private notification: NotificationsService
  ) { }

  ngOnInit(): void {
    this.loadFilters(); // todo: kke: maybe load this after auction list load done?
  }

  // load filter values
  private loadFilters(): void {
    this.filtersSub = this.auctionApi.getFilters$()
      .pipe(startWith(new AuctionFilterModel()))
      .subscribe(
        (result: AuctionFilterModel) => {
          console.log('res: ', result)
          this.filters = result;
        },
        (error: string) => this.notification.error(error)
      );
  }
}
