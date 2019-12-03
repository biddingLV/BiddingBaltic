// angular
import { Component, OnInit } from "@angular/core";

// 3rd lib
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { Subscription } from "rxjs";

// internal
import { AuctionEditComponent } from "ClientApp/src/app/auctions/components/edit/edit.component";
import { AuctionAddMainWizardComponent } from "ClientApp/src/app/auctions/containers/wizard/main/main.component";
import { AuctionDeleteComponent } from "ClientApp/src/app/auctions/components/delete/delete.component";
import { ModalService } from "ClientApp/src/app/core/services/modal/modal.service";
import { FormService } from "ClientApp/src/app/core/services/form/form.service";
import { AuctionListResponseModel } from "ClientApp/src/app/auctions/models/list/auction-list-response.model";
import { AuctionListRequestModel } from "ClientApp/src/app/auctions/models/list/auction-list-request.model";
import { AuctionsService } from "ClientApp/src/app/auctions/services/auctions.service";
import { AuctionListItemModel } from "ClientApp/src/app/auctions/models/list/auction-list-item.model";
import { NotificationsService } from "ClientApp/src/app/core/services/notifications/notifications.service";

@Component({
  templateUrl: "./auction-main.component.html"
})
export class AdminAuctionMainComponent implements OnInit {
  // Component
  mainSubscription: Subscription;

  // API
  auctionTable: AuctionListResponseModel;
  request: AuctionListRequestModel;

  // table
  selected: AuctionListItemModel[] = [];

  // modals
  bsModalRef: BsModalRef;
  subscriptions: Subscription[] = [];

  // pagination || form
  numberRows = 15;
  currentPage = 1;

  /** Search bar - specified text */
  searchText: string;

  constructor(
    private internalModalService: ModalService,
    private internalFormService: FormService,
    private auctionService: AuctionsService,
    private notificationService: NotificationsService,
    private externalModalService: BsModalService
  ) {}

  ngOnInit(): void {
    this.setupInitialAuctionRequest();
    this.loadAuctions();
  }

  addModal(): void {
    const modalConfig = {
      ...this.internalModalService.defaultModalOptions,
      ...{ class: "modal-lg" }
    };

    this.bsModalRef = this.externalModalService.show(
      AuctionAddMainWizardComponent,
      modalConfig
    );

    this.externalModalService.onHide
      .pipe(this.internalModalService.toModalResult())
      .subscribe(result => {
        if (result.success) {
          this.loadAuctions();
        }
      });
  }

  editModal(): void {
    const initialState = {
      selectedAuctionId: this.selected[0].auctionId
    };

    const modalConfig = {
      ...this.internalModalService.defaultModalOptions,
      ...{ initialState: initialState, class: "modal-lg" }
    };

    this.bsModalRef = this.externalModalService.show(
      AuctionEditComponent,
      modalConfig
    );

    this.externalModalService.onHide
      .pipe(this.internalModalService.toModalResult())
      .subscribe(result => {
        if (result.success) {
          this.loadAuctions();
        }
      });
  }

  deleteModal(): void {
    const initialState = {
      selectedAuctions: this.selected
    };

    const modalConfig = {
      ...this.internalModalService.defaultModalOptions,
      ...{ initialState: initialState }
    };

    this.bsModalRef = this.externalModalService.show(
      AuctionDeleteComponent,
      modalConfig
    );

    this.externalModalService.onHide
      .pipe(this.internalModalService.toModalResult())
      .subscribe(result => {
        if (result.success) {
          this.loadAuctions();
        }
      });
  }

  ngOnDestroy(): void {
    if (this.mainSubscription) {
      this.mainSubscription.unsubscribe();
    }
  }

  private setupInitialAuctionRequest(): void {
    this.request = {
      offsetStart: 0,
      offsetEnd: this.numberRows,
      currentPage: this.currentPage,
      sortByColumn: "AuctionName", // by default sort by auction name
      sortingDirection: "asc", // by default ascending
      searchValue: this.searchText
    };
  }

  /** Gets ALL auctions */
  private loadAuctions(): void {
    this.mainSubscription = this.auctionService
      .getAuctionsWithSearch$(this.request)
      .subscribe(
        (response: AuctionListResponseModel) => {
          this.auctionTable = response;
        },
        (error: string) => this.notificationService.error(error)
      );
  }
}
