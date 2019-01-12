import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-auctions-table',
  templateUrl: './auctions.component.html',
  styleUrls: []
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
