// angular
import {
  Component,
  OnInit,
  Output,
  EventEmitter,
  Input,
  ViewChild
} from "@angular/core";

// 3rd lib
import { Subscription } from "rxjs";
import { NgSelectComponent } from "@ng-select/ng-select";

// internal
import { AuctionFilterModel } from "../../models/filters/auction-filter.model";
import { SubCategoryFilterModel } from "../../models/filters/sub-category-filter.model";

@Component({
  selector: "app-auction-filters",
  templateUrl: "./auction-filters.component.html",
  styleUrls: ["./auction-filters.component.scss"]
})
export class AuctionFiltersComponent implements OnInit {
  @Input() filters: AuctionFilterModel;
  @Input() auctionTypes: SubCategoryFilterModel[];

  @Output() topCategoryChange = new EventEmitter<number[]>();
  @Output() subCategoryChange = new EventEmitter<number[]>();

  @ViewChild("typeSelect", { static: false }) typeSelect: NgSelectComponent;

  // component
  filterSubscription: Subscription;

  // used to pass selected filter values to the auction list component
  selectedCategoryIds: number[];
  selectedTypeIds: number[];

  constructor() {}

  ngOnInit(): void {}

  onCategoryChange(categoryIds: number[]): void {
    this.selectedCategoryIds = categoryIds;

    if (categoryIds.length > 0) {
      // filter out based on selected category ids
      this.auctionTypes = this.filters.subCategories.filter(item =>
        categoryIds.includes(item.categoryId)
      );
    } else {
      // nothing selected show the full list
      this.auctionTypes = this.filters.subCategories;
    }

    this.topCategoryChange.emit(categoryIds);
  }

  onTypeChange(typeIds: number[]): void {
    this.selectedTypeIds = typeIds;

    this.subCategoryChange.emit(typeIds);
  }
}
