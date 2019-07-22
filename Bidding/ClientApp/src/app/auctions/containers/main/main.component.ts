// angular
import { Component, OnInit } from '@angular/core';

// 3rd lib
import { Subscription } from 'rxjs';
import { startWith } from 'rxjs/operators';
import * as moment from 'moment-mini';

// internal
import { AuctionsService } from '../../services/auctions.service';
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';
import { AuctionFilterModel } from '../../models/filters/auction-filter.model';
import { SubCategoryFilterModel } from '../../models/filters/sub-category-filter.model';
import { AuctionModel } from '../../models/list/auction.model';
import { AuctionListRequest } from '../../models/list/auction-list-request.model';


@Component({
  selector: 'app-auction-main',
  templateUrl: './main.component.html'
})
export class AuctionMainComponent implements OnInit {
  mainSubscription: Subscription;

  filters: AuctionFilterModel;
  auctionTypes: SubCategoryFilterModel[];

  // used to pass selected filter values to the auction list component
  selectedCategoryIds: number[];
  selectedTypeIds: number[];

  /** Search bar - specified text */
  searchText: string;

  selected?: any[] = [];

  // API
  auctionTable: AuctionModel;
  request: AuctionListRequest;

  // pagination || form
  numberRows = 15;
  currentPage = 1;

  constructor(
    private auctionService: AuctionsService,
    private notificationService: NotificationsService
  ) { }

  ngOnInit(): void {
    this.setupInitialAuctionRequest();
    this.loadActiveAuctions();
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

  // Request Update Events
  updateRequest(property: string, event): void {
    if (property === 'Page') {
      this.request.currentPage = event.page;
    } else {
      this.request.searchValue = event;
      this.request.currentPage = 1;
    }
  }

  // Sort Update Events
  onSortChange(event): void {
    this.request.sortingDirection =
      this.request.sortByColumn === event.column.prop ? this.request.sortingDirection === 'asc' ? 'desc' : 'asc' : 'asc';

    this.request.sortByColumn = event.column.prop;
    this.request.currentPage = 1;
  }

  // todo: kke: is this even needed here?
  onSelectedChange(event): void {
    // this.selectedChange.emit(event);
  }

  onDetailsClick(): void {

  }

  /** Load top & sub categories */
  private loadFilters(): void {
    this.mainSubscription = this.auctionService.getFilters$()
      .pipe(startWith(new AuctionFilterModel()))
      .subscribe(
        (response: AuctionFilterModel) => {
          this.filters = response;
          this.auctionTypes = response.subCategories;
        },
        (error: string) => this.notificationService.error(error)
      );
  }

  private setupInitialAuctionRequest(): void {
    this.request = {
      auctionStartDate: moment().subtract(365, 'days').format('DD/MM/YYYY'),
      auctionEndDate: moment().format('DD/MM/YYYY'),
      sizeOfPage: this.numberRows,
      currentPage: this.currentPage,
      sortByColumn: 'AuctionName', // by default sort by auction name
      sortingDirection: 'asc', // by default ascending
      searchValue: this.searchText
    };
  }

  /** Gets only active auctions */
  private loadActiveAuctions(): void {
    this.mainSubscription = this.auctionService
      .getAuctions$(this.request)
      .subscribe(
        (response: AuctionModel) => { this.auctionTable = response; },
        (error: string) => this.notificationService.error(error)
      );
  }
}
