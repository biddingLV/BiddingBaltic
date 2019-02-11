// angular
import { Component, OnInit, OnDestroy, AfterViewInit, Output, EventEmitter, Input, SimpleChanges } from '@angular/core';

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
export class AuctionListComponent implements OnInit, OnDestroy {
  // pass to child component and
  // pass back to parent component selected array for table
  @Input() selected?: any[] = []; // todo: kke: specify correct type! // note: kke: is this even needed here?
  @Output() selectedChange = new EventEmitter<any>(); // todo: kke: specify correct type!

  // filters - optional
  @Input() categoryIds?: number[];
  @Input() typeIds?: number[];

  // table
  auctionsSub: Subscription;
  auctionTable: AuctionModel;

  // pagination || form
  numberRows: number = 15;
  searchValue: string = '';
  currentPage: number = 1;

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
    console.log('yay, someone just clicked on the details page!')
  }

  // handle filter changes
  // top-category & sub-category
  ngOnChanges(changes: SimpleChanges): void {
    for (const property in changes) {
      switch (!changes[property].firstChange && property) {
        case 'categoryIds':
          this.request.topCategoryIds = changes[property].currentValue;
          this.getAuctions();
          break;
        case 'typeIds':
          console.log('tids');
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
      searchValue: this.searchValue
    };
  }

  private getAuctions(): void {
    console.log(this.request)
    this.auctionsSub = this.auctionApi
      .getAuctions$(this.request)
      .subscribe(
        (result: AuctionModel) => { this.auctionTable = result; },
        (error: string) => this.notification.error(error)
      );
  }
}
