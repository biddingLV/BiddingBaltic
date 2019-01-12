import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';


@Component({
  selector: 'app-admin-table',
  templateUrl: './admin.component.html',
  styleUrls: []
})
export class AdminTableComponent implements OnInit {
  @Input() adminTable: AuctionModel;
  @Input() numberRows: number;
  // @Input() selected: any[];

  @Output() pageChange = new EventEmitter<number>();
  @Output() sortChange = new EventEmitter<boolean>();
  @Output() rowChange = new EventEmitter<number>();

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
    console.log('adminTable', this.adminTable)
  }

  onPageChange(page) {
    console.log('page: ', page)
    this.pageChange.emit(page);
  }

  onRowChange(row) {
    console.log('row: ', row)
    this.rowChange.emit(row);
  }

  onSortChange(event) {
    console.log('event: ', event)
    this.sortChange.emit(event);
  }
}
