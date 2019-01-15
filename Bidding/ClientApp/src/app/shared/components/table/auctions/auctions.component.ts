// angular
import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';

// internal
import { AuctionModel } from 'ClientApp/src/app/auctions/models/list/auction.model';


@Component({
  selector: 'app-auctions-table',
  templateUrl: './auctions.component.html',
  styleUrls: []
})
export class AuctionsTableComponent implements OnInit {
  // table
  @Input() numberRows: number;
  @Input() auctionTable: AuctionModel;

  @Output() pageChange = new EventEmitter<number>();
  @Output() sortChange = new EventEmitter<boolean>();
  @Output() detailsClick = new EventEmitter<boolean>();

  constructor() { }

  ngOnInit() {

  }

  onPageChange(page) {
    this.pageChange.emit(page);
  }

  onSortChange(event) {
    this.sortChange.emit(event);
  }

  onDetailsClick() {
    this.detailsClick.emit();
  }
}
