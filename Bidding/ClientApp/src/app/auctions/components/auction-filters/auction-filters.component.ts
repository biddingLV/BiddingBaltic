// angular
import {
  Component,
  OnInit,
  Output,
  EventEmitter,
  Input,
  ViewChild,
  OnChanges,
  SimpleChanges
} from "@angular/core";

// 3rd lib
import { Subscription } from "rxjs";
import { NgSelectComponent } from "@ng-select/ng-select";

// internal
import { AuctionFilterModel } from "../../models/filters/auction-filter.model";
import { SubCategoryFilterModel } from "../../models/filters/sub-category-filter.model";
import {
  AuctionTopCategoryNames,
  AuctionTopCategoryIds
} from "ClientApp/src/app/core/constants/auction-top-category-constants";

@Component({
  selector: "app-auction-filters",
  templateUrl: "./auction-filters.component.html",
  styleUrls: ["./auction-filters.component.scss"]
})
export class AuctionFiltersComponent implements OnInit, OnChanges {
  @Input() filters: AuctionFilterModel;
  @Input() auctionTypes: SubCategoryFilterModel[];
  @Input() categoryFilter?: string;

  @Output() topCategoryChange = new EventEmitter<number[]>();
  @Output() subCategoryChange = new EventEmitter<number[]>();

  @ViewChild("typeSelect", { static: false }) typeSelect: NgSelectComponent;

  // component
  filterSubscription: Subscription;

  // used to pass selected filter values to the auction list component
  selectedCategoryIds: number[];
  selectedTypeIds: number[];

  // template
  // note: kke: super strange that this only works with Array, not just number!
  preselectedCategory: number[] = [];

  constructor() {}

  ngOnChanges(changes: SimpleChanges): void {
    const categoryFilterChange = changes["categoryFilter"];

    if (categoryFilterChange && categoryFilterChange.currentValue) {
      this.handleCardLinkClick(categoryFilterChange.currentValue);
    }

    return;
  }

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

  private handleCardLinkClick(filterParam: string) {
    if (filterParam === AuctionTopCategoryNames.VehicleCategoryName) {
      this.preselectedCategory.push(AuctionTopCategoryIds.VehicleCategoryId);
    }

    if (filterParam === AuctionTopCategoryNames.PropertyCategoryName) {
      this.preselectedCategory.push(AuctionTopCategoryIds.PropertyCategoryId);
    }

    if (filterParam === AuctionTopCategoryNames.ItemCategoryName) {
      this.preselectedCategory.push(AuctionTopCategoryIds.ItemCategoryId);
    }
  }
}
