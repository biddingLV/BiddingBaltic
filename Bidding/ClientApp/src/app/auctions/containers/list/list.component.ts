import { Component, OnInit, OnDestroy } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Subscription } from 'rxjs';
import { AuctionsService } from '../../services/auctions.service';
import { AuctionModel } from '../../models/list/auction.model';
import { IAuctionListRequest } from '../../models/auction-list-request.model';

@Component({
  selector: 'app-auction-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class AuctionListComponent implements OnInit, OnDestroy {
  pageTitle = 'Auctions';
  auctionsSub: Subscription;
  auctionList: AuctionModel[];
  loading: boolean;
  error: boolean;
  query: '';

  // old logic:
  // public numberRows = 10;
  // public selected = [];

  // API
  // 
  private request: IAuctionListRequest;

  constructor(
    private title: Title,
    private auctionApi: AuctionsService
  ) { }

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

  // searchEvents() {
  //   this.filteredEvents = this.fs.search(this.eventList, this.query, '_id', 'mediumDate');
  // }

  // resetQuery() {
  //   this.query = '';
  //   this.filteredEvents = this.eventList;
  // }

  ngOnDestroy() {
    this.auctionsSub.unsubscribe();
  }
}
