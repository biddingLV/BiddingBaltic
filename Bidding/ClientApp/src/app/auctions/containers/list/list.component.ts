import { Component, OnInit, OnDestroy, AfterViewInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuctionsService } from '../../services/auctions.service';
import { AuctionModel } from '../../models/list/auction.model';
import { IAuctionListRequest } from '../../models/auction-list-request.model';
import { Page } from 'src/app/shared/models/page';
import { CategoryModel } from '../../models/list/category.model';
import { NotificationsService } from 'src/app/core/services/notifications/notifications.service';

@Component({
  selector: 'app-auction-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class AuctionListComponent implements OnInit, OnDestroy, AfterViewInit {
  // table
  auctionsSub: Subscription;
  auctionTable: AuctionModel;

  // pagination
  numberRows = 10;

  //filters
  categories: CategoryModel[];

  //utility
  loading: boolean;

  page = new Page();

  // old logic:
  // public numberRows = 10;
  // public selected = [];

  // API
  // 
  request: IAuctionListRequest;

  constructor(
    private auctionApi: AuctionsService,
    private notification: NotificationsService
  ) {
    this.page.pageNumber = 0;
    this.page.size = 20;
  }

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
    this.loadCategoryFilter();
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
        (result: AuctionModel) => { this.auctionTable = result; },
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
