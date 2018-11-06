import { Component, OnInit, OnDestroy, AfterViewInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Subscription } from 'rxjs';
import { AuctionsService } from '../../services/auctions.service';
import { AuctionModel } from '../../models/list/auction.model';
import { IAuctionListRequest } from '../../models/auction-list-request.model';
import { Page } from 'src/app/shared/models/page';
import { CategoryModel } from '../../models/list/category.model';

@Component({
  selector: 'app-auction-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class AuctionListComponent implements OnInit, OnDestroy, AfterViewInit {
  pageTitle = 'Auctions';
  auctionsSub: Subscription;
  auctionList: AuctionModel[];
  categories: CategoryModel[];
  loading: boolean;
  error: boolean;
  query: '';

  page = new Page();

  // old logic:
  // public numberRows = 10;
  // public selected = [];

  // API
  // 
  private request: IAuctionListRequest;

  constructor(
    private title: Title,
    private auctionApi: AuctionsService
  ) {
    this.page.pageNumber = 0;
    this.page.size = 20;
  }

  ngOnInit() {
    this.title.setTitle(this.pageTitle);
    this.getAuctionList();
  }

  private getAuctionList() {
    this.loading = true;

    // Get all (admin) events
    this.auctionsSub = this.auctionApi
      .getAuctions$(this.request)
      .subscribe(
        res => {
          this.auctionList = res;
          this.loading = false;
        },
        err => {
          console.error(err);
          this.loading = false;
          this.error = true;
        }
      );
  }

  ngAfterViewInit() {
    this.loadCategoryFilter();
  }

  private loadCategoryFilter() {
    // get all categories for the filter
    this.auctionsSub = this.auctionApi
      .getCategories$()
      .subscribe(
        res => {
          this.categories = res;
        },
        err => {
          console.error(err);
          this.error = true;
        }
      );
  }

  ngOnDestroy() {
    this.auctionsSub.unsubscribe();
  }
}
