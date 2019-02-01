// angular
import { Component, OnInit, OnDestroy, AfterViewInit, Output, EventEmitter, Input } from '@angular/core';

// 3rd lib
import { Subscription } from 'rxjs';

// internal
import { AuctionsService } from '../../services/auctions.service';
import { AuctionModel } from '../../models/list/auction.model';
import { AuctionListRequest } from '../../models/list/auction-list-request.model';
import { CategoryModel } from '../../models/filters/category.model';
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';


@Component({
  selector: 'app-auction-list',
  templateUrl: './list.component.html',
  styleUrls: []
})
export class AuctionListComponent implements OnInit, OnDestroy, AfterViewInit {
  // pass to child component and
  // pass back to parent component selected array for table
  @Input() selected?: any[] = []; // todo: kke: specify correct type!
  @Output() selectedChange = new EventEmitter<any>(); // todo: kke: specify correct type!

  // table
  auctionsSub: Subscription;
  auctionTable: AuctionModel;

  // pagination || form
  numberRows: number = 15;
  searchValue: string = '';
  currentPage: number = 0;

  // filters
  categories: CategoryModel[];

  // API
  request: AuctionListRequest;

  constructor(
    private auctionApi: AuctionsService,
    private notification: NotificationsService
  ) { }

  ngOnInit(): void {
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

  onSelectedChange(event): void {
    console.log('auction list - event: ', event)
    this.selectedChange.emit(event);
  }

  onDetailsClick(): void {
    console.log('yay, someone just clicked on the details page!')
  }

  ngOnDestroy(): void {
    if (this.auctionsSub) {
      this.auctionsSub.unsubscribe();
    }
  }

  ngAfterViewInit(): void {
    this.loadCategoryFilter();
  }

  private setupAuctionRequest(): void {
    this.request = {
      starDate: new Date(),
      endDate: new Date(),
      sizeOfPage: this.numberRows,
      currentPage: this.currentPage,
      sortByColumn: 'Name',
      sortingDirection: 'asc',
      searchValue: this.searchValue
    };
  }

  private getAuctions(): void {
    this.setupAuctionRequest();

    this.auctionsSub = this.auctionApi
      .getAuctions$(this.request)
      .subscribe(
        (result: AuctionModel) => { this.auctionTable = result; },
        (error: string) => this.notification.error(error)
      );
  }

  private loadCategoryFilter(): void {
    // get all categories for the filter
    this.auctionsSub = this.auctionApi
      .getCategories$()
      .subscribe(
        (result: CategoryModel[]) => { this.categories = result; },
        (error: string) => this.notification.error(error)
      );
  }
}
