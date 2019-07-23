// angular
import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';

// internal
import { UserListResponseModel } from '../../../models/list/user-list-response.model';
import { UserListItemModel } from '../../../models/list/user-list-item.model';


@Component({
  selector: 'app-user-table',
  templateUrl: './user-table.component.html'
})
export class UserTableComponent implements OnInit {
  // table
  @Input() numberRows: number;
  @Input() userTable: UserListResponseModel;
  @Input() selected: UserListItemModel[];

  @Output() pageChange = new EventEmitter<number>();
  @Output() sortChange = new EventEmitter<boolean>();
  @Output() detailsClick = new EventEmitter<boolean>();
  @Output() selectedChange = new EventEmitter<any>();

  constructor() { }

  ngOnInit(): void {

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
