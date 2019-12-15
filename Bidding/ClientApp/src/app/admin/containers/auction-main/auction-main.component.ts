// angular
import { Component, OnInit, ViewChild } from "@angular/core";

// 3rd lib
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { Subscription } from "rxjs";

// internal
import { AuctionEditComponent } from "ClientApp/src/app/auctions/components/edit/edit.component";
import { AuctionAddMainWizardComponent } from "ClientApp/src/app/auctions/containers/wizard/main/main.component";
import { AuctionDeleteComponent } from "ClientApp/src/app/auctions/components/delete/delete.component";
import { ModalService } from "ClientApp/src/app/core/services/modal/modal.service";
import { AuctionListItemModel } from "ClientApp/src/app/auctions/models/list/auction-list-item.model";
import { AuctionListComponent } from "ClientApp/src/app/auctions/containers/list/list.component";

@Component({
  templateUrl: "./auction-main.component.html"
})
export class AdminAuctionMainComponent implements OnInit {
  // Component
  mainSubscription: Subscription;

  // table
  selectedAuctions: AuctionListItemModel[] = [];

  // modals
  bsModalRef: BsModalRef;

  // template
  selectedCategoryIds: number[] = [];
  selectedTypeIds: number[] = [];

  /** Show or hide select all checkbox column in auction table. */
  showSelectAllCheckboxColumn: boolean = true;

  @ViewChild(AuctionListComponent, { static: false })
  auctionList: AuctionListComponent;

  constructor(
    private internalModalService: ModalService,
    private externalModalService: BsModalService
  ) {}

  ngOnInit(): void {}

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
          this.auctionList.loadActiveAuctions();
        }
      });
  }

  editModal(): void {
    const initialState = {
      selectedAuctionId: this.selectedAuctions[0].auctionId
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
          this.auctionList.loadActiveAuctions();
        }
      });
  }

  deleteModal(): void {
    const initialState = {
      selectedAuctions: this.selectedAuctions
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
          this.auctionList.loadActiveAuctions();
        }
      });
  }

  onSelectedChange(selected: AuctionListItemModel[]): void {
    this.selectedAuctions = selected;
  }

  ngOnDestroy(): void {
    if (this.mainSubscription) {
      this.mainSubscription.unsubscribe();
    }
  }
}
