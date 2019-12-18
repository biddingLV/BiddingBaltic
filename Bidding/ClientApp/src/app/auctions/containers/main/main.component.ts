// angular
import { Component, OnInit, OnDestroy } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

// 3rd lib
import { Subscription } from "rxjs";
import { startWith } from "rxjs/operators";

// internal
import { AuctionsService } from "../../services/auctions.service";
import { NotificationsService } from "ClientApp/src/app/core/services/notifications/notifications.service";
import { AuctionFilterModel } from "../../models/filters/auction-filter.model";
import { SubCategoryFilterModel } from "../../models/filters/sub-category-filter.model";
import { AuctionListResponseModel } from "../../models/list/auction-list-response.model";
import { AuctionListRequestModel } from "../../models/list/auction-list-request.model";
import { BreadcrumbItem } from "ClientApp/src/app/shared/models/breadcrumb-item.model";

@Component({
  selector: "app-auction-main",
  templateUrl: "./main.component.html",
  styleUrls: ["./main.component.scss"]
})
export class AuctionMainComponent implements OnInit, OnDestroy {
  // component
  mainSubscription: Subscription;

  // used to pass selected filter values to the auction list component
  selectedCategoryIds: number[];
  selectedTypeIds: number[];
  specifiedSearchText: string;

  selected?: any[] = [];

  // API
  auctionTable: AuctionListResponseModel;
  auctionListRequest: AuctionListRequestModel;

  // pagination || form
  numberRows = 15;
  currentPage = 1;

  // filters
  filters: AuctionFilterModel;
  auctionTypes: SubCategoryFilterModel[];

  // breadcrumbs
  breadcrumbs: BreadcrumbItem[];

  // template
  categoryFilter: string = "";

  constructor(
    private auctionService: AuctionsService,
    private notificationService: NotificationsService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.generateBreadcrumbs();
    this.loadFilters();
    this.handleCategoryFilterChange();
  }

  /** Called on auction search event */
  onSearch(searchText: string): void {
    this.specifiedSearchText = searchText;
  }

  onTopCategoryChange(categoryIds: number[]) {
    this.selectedCategoryIds = categoryIds;
  }

  onSubCategoryChange(typeIds: number[]) {
    this.selectedTypeIds = typeIds;
  }

  ngOnDestroy(): void {
    if (this.mainSubscription) {
      this.mainSubscription.unsubscribe();
    }
  }

  private generateBreadcrumbs() {
    this.breadcrumbs = [
      {
        name: "Sākumlapa",
        url: "/"
      },
      {
        name: "Izsoles",
        url: "/izsoles"
      }
    ];
  }

  /** Load top & sub categories */
  private loadFilters(): void {
    this.mainSubscription = this.auctionService
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

  private handleCategoryFilterChange(): void {
    this.mainSubscription = this.activatedRoute.queryParams.subscribe(
      params => {
        if (Object.keys(params).length) {
          let filterParam: string = params["filtrs"];
          if (filterParam) this.categoryFilter = filterParam;
        }
      }
    );
  }
}
