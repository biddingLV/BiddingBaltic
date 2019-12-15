// angular
import { Component, OnInit, Input, EventEmitter, Output } from "@angular/core";

// internal
import { AuctionListItemModel } from "../../models/list/auction-list-item.model";
import { AuctionListResponseModel } from "../../models/list/auction-list-response.model";

@Component({
  selector: "app-auction-table",
  templateUrl: "./auction-table.component.html"
})
export class AuctionTableComponent implements OnInit {
  // template
  @Input() numberRows?: number = 15;
  @Input() auctionTable: AuctionListResponseModel;

  /** This input param cant be initialized empty on this level only on parent level */
  @Input() selected: AuctionListItemModel[];

  /** Show or hide select all checkbox column in template. */
  @Input() showSelectAllCheckboxColumn: boolean;

  @Output() pageChange = new EventEmitter<number>();
  @Output() sortChange = new EventEmitter<boolean>();
  @Output() selectedChange = new EventEmitter<AuctionListItemModel[]>();

  constructor() {}

  ngOnInit(): void {}

  onPageChange(event): void {
    this.pageChange.emit(event.page);
  }

  onSortChange(event: boolean): void {
    this.sortChange.emit(event);
  }

  onSelect(selectedItems: { selected }): void {
    this.selectedChange.emit(selectedItems.selected);
  }
}
