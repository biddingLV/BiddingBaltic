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

  selected?: any[] = [];

  // API
  auctionTable: AuctionModel;
  auctionListRequest: AuctionListRequest;

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
  onSearch(text: string): void {
    if (text != undefined) {
      this.auctionListRequest.searchValue = text;
    } else {
      this.auctionListRequest.searchValue = '';
    }

    this.updateColumns(1);
  }

  // Request Update Events
  updateRequest(property: string, event): void {
    if (property === 'Page') {
      this.auctionListRequest.currentPage = event.page;
    } else {
      this.auctionListRequest.searchValue = event;
      this.auctionListRequest.currentPage = 1;
    }
  }

  // Sort Update Events
  onSortChange(event): void {
    this.auctionListRequest.sortingDirection =
      this.auctionListRequest.sortByColumn === event.column.prop ? this.auctionListRequest.sortingDirection === 'asc' ? 'desc' : 'asc' : 'asc';

    this.auctionListRequest.sortByColumn = event.column.prop;
    this.auctionListRequest.currentPage = 1;
  }

  // todo: kke: is this even needed here?
  onSelectedChange(event): void {
    // this.selectedChange.emit(event);
  }

  onDetailsClick(): void {

  }

  private updateColumns(page: number): void {
    this.auctionListRequest.offsetStart = page;

    this.loadActiveAuctions();
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
    this.auctionListRequest = {
      auctionStartDate: moment().subtract(365, 'days').format('DD/MM/YYYY'),
      auctionEndDate: moment().format('DD/MM/YYYY'),
      offsetStart: 0,
      offsetEnd: this.numberRows,
      currentPage: this.currentPage,
      sortByColumn: 'AuctionName', // by default sort by auction name
      sortingDirection: 'asc', // by default ascending
      searchValue: ''
    };
  }

  /** Gets only active auctions */
  private loadActiveAuctions(): void {
    this.mainSubscription = this.auctionService
      .getAuctions$(this.auctionListRequest)
      .subscribe(
        (response: AuctionModel) => { this.auctionTable = response; },
        (error: string) => this.notificationService.error(error)
      );
  }
}
