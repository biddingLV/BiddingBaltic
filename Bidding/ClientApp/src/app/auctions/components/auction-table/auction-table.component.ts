// angular
import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';

// internal
import { AuctionModel } from '../../models/list/auction.model';


@Component({
  selector: 'app-auction-table',
  templateUrl: './auction-table.component.html'
})
export class AuctionTableComponent implements OnInit {
  // table
  @Input() numberRows: number;
  @Input() auctionTable: AuctionModel;
  @Input() selected: any[];

  /**
   * Can be passed from parent component to show or hide select all checkbox column in template.
   * By default don't show.
   */
  @Input() showSelectAllCheckboxColumn = false;

  @Output() pageChange = new EventEmitter<number>();
  @Output() sortChange = new EventEmitter<boolean>();
  @Output() detailsClick = new EventEmitter<boolean>();
  @Output() selectedChange = new EventEmitter<any>();

  constructor() { }

  ngOnInit(): void {
    console.log('table: ', this.auctionTable);
  }

  onPageChange(page: number): void {
    this.pageChange.emit(page);
  }

  onSortChange(event: boolean): void {
    this.sortChange.emit(event);
  }

  onDetailsClick(): void {
    this.detailsClick.emit();
  }

  onSelect({ selected }): void {

    this.selected.splice(0, this.selected.length);
    this.selected.push(...selected);

    this.selectedChange.emit(selected);
  }
}
