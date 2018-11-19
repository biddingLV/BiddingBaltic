import { Component, OnInit, OnDestroy, AfterViewInit } from '@angular/core';
import { Subscription } from 'rxjs';

// error handling
import { NotificationsService } from 'src/app/core/services/notifications/notifications.service';
import { AuctionModel } from 'src/app/auctions/models/list/auction.model';
import { AuctionsService } from 'src/app/auctions/services/auctions.service';
import { AuctionListRequest } from 'src/app/auctions/models/list/auction-list-request.model';
import { CategoryModel } from 'src/app/auctions/models/filters/category.model';

@Component({
  selector: 'app-admin-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class AdminListComponent implements OnInit, OnDestroy, AfterViewInit {
  // table
  auctionsSub: Subscription;
  adminTable: AuctionModel;

  // pagination
  numberRows = 10;

  //filters
  categories: CategoryModel[];

  //utility
  loading: boolean;

  // API
  request: AuctionListRequest;

  constructor(
    private auctionApi: AuctionsService,
    private notification: NotificationsService
  ) { }

  ngOnInit() {
    this.setupRequest();
    this.getAuctionList();
  }

  // Request Update Events
  updateRequest(property: string, event) {
    if (property === 'Page') {
      this.request.CurrentPage = event.page;
    } else {
      this.request[property] = (event === undefined) || (event === 'undefined') ? '' : event;
      this.request.CurrentPage = 1;
    }

    this.getAuctionList();
  }

  // Sort Update Events
  onSortChange(event): void {
    this.request.SortingDirection = this.request.SortByColumn === event.column.prop ?
      this.request.SortingDirection === 'asc' ? 'desc' : 'asc'
      : 'asc'; // TODO: HS: maybe this can still be new value
    this.request.SortByColumn = event.column.prop;
    this.request.CurrentPage = 1;

    this.getAuctionList();
  }

  ngOnDestroy() {
    this.auctionsSub.unsubscribe();
  }

  ngAfterViewInit() {
    // this.loadCategoryFilter();
  }

  private setupRequest(): void {
    // todo: kke: improve this
    this.request = {
      StarDate: new Date(),
      EndDate: new Date(),
      SizeOfPage: this.numberRows,
      CurrentPage: 1,
      SortByColumn: 'Name',
      SortingDirection: 'asc',
      SearchValue: ''
    };
  }

  private getAuctionList() {
    // this.loading = true;
    console.log('request', this.request)
    // Get all (admin) events
    this.auctionsSub = this.auctionApi
      .getAuctions$(this.request)
      .subscribe(
        (result: AuctionModel) => { this.adminTable = result; },
        (error: string) => this.notification.error(error)
      );
  }

  private loadCategoryFilter() {
    // get all categories for the filter
    this.auctionsSub = this.auctionApi
      .getCategories$()
      .subscribe(
        (result: CategoryModel[]) => { this.categories = result; },
        (error: string) => this.notification.error(error)
      );
  }
}
