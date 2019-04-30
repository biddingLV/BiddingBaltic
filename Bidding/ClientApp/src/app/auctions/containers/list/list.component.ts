// angular
import { Component, OnInit, OnDestroy, Output, EventEmitter, Input, SimpleChanges, OnChanges } from '@angular/core';

// 3rd lib
import { Subscription } from 'rxjs';
import * as moment from 'moment-mini';

// internal
import { AuctionsService } from '../../services/auctions.service';
import { AuctionModel } from '../../models/list/auction.model';
import { AuctionListRequest } from '../../models/list/auction-list-request.model';
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';


@Component({
  selector: 'app-auction-list',
  templateUrl: './list.component.html',
  styleUrls: []
})
export class AuctionListComponent implements OnInit, OnDestroy, OnChanges {
  @Input() selected?: any[] = []; // todo: kke: specify correct type! // note: kke: is this even needed here?
  @Output() selectedChange = new EventEmitter<any>(); // todo: kke: specify correct type!

  /** Top-category filter */
  @Input() categoryIds?: number[];

  /** Sub-category filter */
  @Input() typeIds?: number[];

  /** Search bar text */
  @Input() searchText = '';

  // table
  auctionsSub: Subscription;
  auctionTable: AuctionModel;

  // pagination || form
  numberRows = 15;
  currentPage = 1;

  // API
  request: AuctionListRequest;

  constructor(
    private auctionApi: AuctionsService,
    private notification: NotificationsService
  ) { }

  ngOnInit(): void {
    this.setupInitialAuctionRequest();
    this.getAuctions();
  }

  // Request Update Events
  updateRequest(property: string, event): void {
    if (property === 'Page') {
      this.request.currentPage = event.page;
    } else {
      this.request.searchValue = event;
      this.request.currentPage = 1;
    }

    this.getAuctions();
  }

  // Sort Update Events
  onSortChange(event): void {
    this.request.sortingDirection =
      this.request.sortByColumn === event.column.prop ? this.request.sortingDirection === 'asc' ? 'desc' : 'asc' : 'asc';

    this.request.sortByColumn = event.column.prop;
    this.request.currentPage = 1;

    this.getAuctions();
  }

  // todo: kke: is this even needed here?
  onSelectedChange(event): void {
    this.selectedChange.emit(event);
  }

  onDetailsClick(): void {

  }

  /**
   * Handle filter changes for top-category & sub-category
   * @param changes
   */
  ngOnChanges(changes: SimpleChanges): void {
    for (const property in changes) {
      switch (!changes[property].firstChange && property) {
        case 'categoryIds':
          this.request.topCategoryIds = changes[property].currentValue;
          this.getAuctions();
          break;
        case 'typeIds':
          this.request.typeIds = changes[property].currentValue;
          this.getAuctions();
          break;
        case 'searchText':
          this.request.searchValue = changes[property].currentValue;
          this.getAuctions();
          break;
        default:
          break;
      }
    }
  }

  ngOnDestroy(): void {
    if (this.auctionsSub) {
      this.auctionsSub.unsubscribe();
    }
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

  private getAuctions(): void {
    this.auctionsSub = this.auctionApi
      .getAuctions$(this.request)
      .subscribe(
        (result: AuctionModel) => { this.auctionTable = result; },
        (error: string) => this.notification.error(error)
      );
  }
}
