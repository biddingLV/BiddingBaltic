import { Component, OnInit, OnDestroy } from '@angular/core';
import { IAuctionListRequest } from '../../models/interfaces/auction-list-request.model';
import { IAuctionListResponse } from '../../models/interfaces/auction-list-response.model';
import { AuctionsService } from '../../services/auctions.service';
import { UtilsService } from '../../../core';
import { Subscription } from 'rxjs';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-auction-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class AuctionListComponent implements OnInit, OnDestroy {
  pageTitle = 'Auctions';
  auctionListSub: Subscription;
  auctionList: IAuctionListResponse[];
  filteredAuctions: IAuctionListResponse[];
  loading: boolean;
  error: boolean;
  query: '';

  public numberRows = 10;
  // public selected = [];

  // API
  private request: IAuctionListRequest;

  constructor(private title: Title, public utils: UtilsService, private auctionApi: AuctionsService) { }

  public ngOnInit() {
    this.title.setTitle(this.pageTitle);
    this.initializeRequest();
    this._getAuctionList();
  }

  // Data Table Events
  public onSortChange(event): void {
    this.request.SortByColumn = event.column.prop;
    this.request.SortingDirection = event.newValue;
    this._getAuctionList();
  }

  public onPageChange(event): void {
    this.request.OffsetStart = event.page;
    this._getAuctionList();
  }

  public onRowChange(event): void {
    this.request.OffsetStart = 1;
    this.request.OffsetEnd = event;
    this._getAuctionList();
  }

  // Private
  private initializeRequest(): void {
    this.request = {
      OffsetEnd: this.numberRows,
      OffsetStart: 1,
      SortByColumn: 'Brand',
      SortingDirection: 'asc',
      SearchValue: 'BMW'
    };
  }

  // Private API Get Functions
  private _getAuctionList() {
    this.loading = true;

    this.auctionListSub = this.auctionApi
      .getAuctions$(this.request)
      .subscribe(
        res => {
          this.auctionList = res;
          this.filteredAuctions = res;
          this.loading = false;
        },
        err => {
          console.error(err);
          this.loading = false;
          this.error = true;
        }
      );
  }

  ngOnDestroy() {
    this.auctionListSub.unsubscribe();
  }
}
