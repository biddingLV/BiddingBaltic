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

  filters: AuctionFilterModel;
  auctionTypes: SubCategoryFilterModel[];

  // used to pass selected filter values to the auction list component
  selectedCategoryIds: number[];
  selectedTypeIds: number[];

  /** Search bar - specified text */
  searchText: string;

  constructor(
    private auctionApi: AuctionsService,
    private notification: NotificationsService
  ) { }

  ngOnInit(): void {
    this.loadFilters();
  }

  // on top category change - select
  onCategoryChange(categoryIds: number[]): void {
    this.selectedCategoryIds = categoryIds;

    if (categoryIds.length > 0) {
      // filter out based on selected category ids
      this.auctionTypes = this.filters.subCategories.filter(item => categoryIds.includes(item.categoryId));
    } else {
      // nothing selected show the full list
      this.auctionTypes = this.filters.subCategories;
    }
  }

  onTypeChange(typeIds: number[]): void {
    this.selectedTypeIds = typeIds;
  }

  /** Called on auction search event */
  onAuctionSearch(searchText: string) {
    this.searchText = searchText;
  }

  /** Load top & sub categories */
  private loadFilters(): void {
    this.filtersSub = this.auctionApi.getFilters$()
      .pipe(startWith(new AuctionFilterModel()))
      .subscribe(
        (result: AuctionFilterModel) => {
          this.filters = result;
          this.auctionTypes = result.subCategories;
        },
        (error: string) => this.notification.error(error)
      );
  }
}
