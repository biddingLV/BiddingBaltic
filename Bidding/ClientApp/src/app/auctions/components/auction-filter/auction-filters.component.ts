// angular
import { Component, OnInit } from "@angular/core";

// 3rd lib
import { Subscription } from "rxjs";
import { startWith } from "rxjs/operators";

// internal
import { AuctionsService } from "../../services/auctions.service";
import { NotificationsService } from "ClientApp/src/app/core";
import { AuctionFilterModel } from "../../models/filters/auction-filter.model";
import { SubCategoryFilterModel } from "../../models/filters/sub-category-filter.model";

@Component({
  selector: "app-auction-filters",
  templateUrl: "./auction-filters.component.html",
  styleUrls: ["./auction-filters.component.scss"]
})
export class AuctionFiltersComponent implements OnInit {
  // component
  filterSubscription: Subscription;

  filters: AuctionFilterModel;
  auctionTypes: SubCategoryFilterModel[];

  // used to pass selected filter values to the auction list component
  selectedCategoryIds: number[];
  selectedTypeIds: number[];

  constructor(
    private auctionService: AuctionsService,
    private notificationService: NotificationsService
  ) {}

  ngOnInit(): void {
    this.loadFilters();
  }

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
  }

  onTypeChange(typeIds: number[]): void {
    this.selectedTypeIds = typeIds;
  }

  /** Load top & sub categories */
  private loadFilters(): void {
    this.filterSubscription = this.auctionService
      .getFilters$()
      .pipe(startWith(new AuctionFilterModel()))
      .subscribe(
        (response: AuctionFilterModel) => {
          this.filters = response;
          this.auctionTypes = response.subCategories;
        },
        (error: string) => this.notificationService.error(error)
      );
  }
}
