// angular
import { Component, OnInit } from '@angular/core';

// 3rd lib
import { Subscription } from 'rxjs';
import { startWith } from 'rxjs/operators';

// internal
import { AuctionsService } from '../../services/auctions.service';
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';
import { AuctionFilterModel } from '../../models/filters/auction-filter.model';
import { SubCategoryFilterModel } from '../../models/filters/sub-category-filter.model';


@Component({
  selector: 'app-auction-main',
  templateUrl: './main.component.html',
  styleUrls: []
})
export class AuctionMainComponent implements OnInit {
  // API
  filtersSub: Subscription;

  // filters
  // filter - model
  filters: AuctionFilterModel;
  auctionTypes: SubCategoryFilterModel[];

  // used to pass selected filter values to the auction list component
  selectedCategoryIds: number[];

  constructor(
    private auctionApi: AuctionsService,
    private notification: NotificationsService
  ) { }

  // todo: kke: maybe load this after auction list load done?
  // todo: kke: maybe something like a flag from child list component and only then initialize filter load?
  // todo: so-so improvement - ms?
  ngOnInit(): void {
    this.loadFilters();
  }

  // on top category change - select
  onCategoryChange(categoryIds: number[]): void {
    this.selectedCategoryIds = categoryIds;

    if (categoryIds.length > 0) {
      // filter out based on selected category ids
      this.auctionTypes = this.filters.subCategories.filter(item => categoryIds.indexOf(item.categoryId) < 0); // todo: kke: not working as needed still!
    } else {
      // nothing selected show the full list
      this.auctionTypes = this.filters.subCategories;
    }
  }

  // load filter values
  private loadFilters(): void {
    this.filtersSub = this.auctionApi.getFilters$()
      .pipe(startWith(new AuctionFilterModel()))
      .subscribe(
        (result: AuctionFilterModel) => {
          console.log('res: ', result)
          this.filters = result;
          this.auctionTypes = result.subCategories;
        },
        (error: string) => this.notification.error(error)
      );
  }
}
