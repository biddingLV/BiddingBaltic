import { Component, OnInit, OnDestroy, AfterViewInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

// rxjs
import { Subscription } from 'rxjs';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';

// custom
import { AuctionsService } from '../../services/auctions.service';
import { AuctionModel } from '../../models/list/auction.model';
import { IAuctionListRequest } from '../../models/auction-list-request.model';
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

  // pagination || form
  numberRows = 15;
  searchValue = '';
  currentPage = 0;

  // filters
  categories: CategoryModel[];

  // utility
  loading: boolean;

  // API
  request: IAuctionListRequest;

  constructor(
    private auctionApi: AuctionsService,
    private notification: NotificationsService,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.setupRequest();
    this.getAuctionList();
  }

  // Request Update Events
  updateRequest(property: string, event) {
    if (property === 'Page') {
      this.request.currentPage = event.page;
    } else {
      this.request.searchValue = event;
      this.request.currentPage = 1;
    }

    this.getAuctionList();
  }

  // Sort Update Events
  onSortChange(event): void {
    this.request.sortingDirection =
      this.request.sortByColumn === event.column.prop ? this.request.sortingDirection === 'asc' ? 'desc' : 'asc' : 'asc';

    this.request.sortByColumn = event.column.prop;
    this.request.currentPage = 1;

    this.getAuctionList();
  }

  onDetailsClick(): void {
    console.log('yay, someone just clicked on the details page!')
  }

  ngOnDestroy() {
    if (this.auctionsSub) {
      this.auctionsSub.unsubscribe();
    }
  }

  ngAfterViewInit() {
    this.loadCategoryFilter();
  }

  private setupRequest(): void {
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

  private getAuctionList() {
    // this.loading = true;

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
