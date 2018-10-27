import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { IAuctionListResponse } from '../../../../auctions/models/auction-list-response.model';

@Component({
  selector: 'app-auctions-table',
  templateUrl: './auctions.component.html',
  styleUrls: ['./auctions.component.scss']
})
export class AuctionsTableComponent implements OnInit {
  @Input() public list: IAuctionListResponse;
  @Input() public numberRows: number;
  @Input() public selected: any[];

  // @Output() public pageChange = new EventEmitter<number>();
  // @Output() public sortChange = new EventEmitter<boolean>();
  // @Output() public rowChange = new EventEmitter<number>();

  private items = [
    { name: '5 per page', value: 5 },
    { name: '10 per page', value: 10 },
    { name: '25 per page', value: 25 },
    { name: '50 per page', value: 50 },
    { name: '100 per page', value: 100 },
    { name: 'All', value: 0 }
  ];

  constructor() { }

  ngOnInit() {
    console.log(this.list);
  }

  // onPageChange(page) {
  //   this.pageChange.emit(page);
  // }

  // onRowChange(row) {
  //   this.rowChange.emit(row);
  // }

  // onSortChange(event) {
  //   this.sortChange.emit(event);
  // }

}
