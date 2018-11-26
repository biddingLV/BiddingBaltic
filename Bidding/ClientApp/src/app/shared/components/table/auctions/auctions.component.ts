import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { AuctionModel } from 'src/app/auctions/models/list/auction.model';

@Component({
  selector: 'app-auctions-table',
  templateUrl: './auctions.component.html',
  styleUrls: ['./auctions.component.scss']
})
export class AuctionsTableComponent implements OnInit {
  @Input() auctionTable: AuctionModel;
  @Input() numberRows: number;

  @Output() pageChange = new EventEmitter<number>();
  @Output() sortChange = new EventEmitter<boolean>();
  @Output() detailsClick = new EventEmitter<boolean>();

  creator: string;

  constructor() { }

  ngOnInit() {
    // not working!
    this.creator = this.auctionTable.creatorFirstName + this.auctionTable.creatorLastName;
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
