// angular
import { Component, OnInit } from "@angular/core";

// 3rd lib
import { Subscription } from "rxjs";

// internal
import { AuctionsService } from "ClientApp/src/app/auctions/services/auctions.service";
import { NotificationsService } from "ClientApp/src/app/core";
import { startWith } from "rxjs/operators";
import { AuctionFilterModel } from "ClientApp/src/app/auctions/models/filters/auction-filter.model";
import { SubCategoryFilterModel } from "ClientApp/src/app/auctions/models/filters/sub-category-filter.model";

@Component({
  selector: "app-home-main",
  templateUrl: "./main.component.html"
})
export class HomeMainComponent implements OnInit {
  // used to pass selected filter values to the auction list component
  selectedCategoryIds: number[];
  selectedTypeIds: number[];

  // component
  homeSubscription: Subscription;

  // filters
  filters: AuctionFilterModel;
  auctionTypes: SubCategoryFilterModel[];

  constructor(
    private auctionService: AuctionsService,
    private notificationService: NotificationsService
  ) {}

  ngOnInit(): void {
    this.loadFilters();
  }

  onTopCategoryChange(categoryIds: number[]) {
    this.selectedCategoryIds = categoryIds;
  }

  onSubCategoryChange(typeIds: number[]) {
    this.selectedTypeIds = typeIds;
  }

  /** Load top & sub categories */
  private loadFilters(): void {
    this.homeSubscription = this.auctionService
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
