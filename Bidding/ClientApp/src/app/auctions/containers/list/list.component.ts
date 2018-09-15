import { Component, OnInit } from '@angular/core';
import { IAuctionListRequest } from '../../models/auction-list-request.model';
import { IAuctionListResponse } from '../../models/auction-list-response.model';
import { AuctionsService } from '../../services/auctions.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-auction-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class AuctionListComponent implements OnInit {

  public numberRows = 10;
  public selected = [];

  // API
  public response$: Observable<IAuctionListResponse>;
  private request: IAuctionListRequest;
  public Auctions: IAuctionListResponse;

  constructor(private auctionApi: AuctionsService) { }

  public ngOnInit() {
    this.initRequest();
    this.getAuctions();
  }

  // Data Table Events
  public onSortChange(event): void {
    this.request.SortByColumn = event.column.prop;
    this.request.SortingDirection = event.newValue;
    this.getAuctions();
  }

  public onPageChange(event): void {
    this.request.OffsetStart = event.page;
    this.getAuctions();
  }

  public onRowChange(event): void {
    this.request.OffsetStart = 1;
    this.request.OffsetEnd = event;
    this.getAuctions();
  }

  // Private
  private initRequest(): void {
    this.request = {
      OffsetEnd: this.numberRows,
      OffsetStart: 1,
      SortByColumn: 'Brand',
      SortingDirection: 'asc',
      SearchValue: 'BMW'
    };
  }

  // Private API Get Functions
  private getAuctions(): void {
    this.response$ = this.auctionApi.getAuctions(this.request);
    this.response$.subscribe((data: IAuctionListResponse) => {
      this.Auctions = { ...data };
    });
  }
}
