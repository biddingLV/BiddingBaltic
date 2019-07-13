// angular
import { Component, OnInit } from '@angular/core';

// 3rd lib
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';
import * as moment from 'moment-mini';

// internal
import { AuctionEditComponent } from 'ClientApp/src/app/auctions/components/edit/edit.component';
import { AuctionAddMainWizardComponent } from 'ClientApp/src/app/auctions/containers/wizard/main/main.component';
import { AuctionDeleteComponent } from 'ClientApp/src/app/auctions/components/delete/delete.component';
import { ModalService } from 'ClientApp/src/app/core/services/modal/modal.service';
import { FormService } from 'ClientApp/src/app/core/services/form/form.service';
import { AuctionModel } from 'ClientApp/src/app/auctions/models/list/auction.model';
import { AuctionListRequest } from 'ClientApp/src/app/auctions/models/list/auction-list-request.model';
import { AuctionsService } from 'ClientApp/src/app/auctions/services/auctions.service';
import { NotificationsService } from 'ClientApp/src/app/core';


@Component({
  templateUrl: './auction-main.component.html'
})
export class AdminAuctionMainComponent implements OnInit {
  // Component
  mainSubscription: Subscription;

  // API
  auctionTable: AuctionModel;
  request: AuctionListRequest;

  // table
  selected = [];

  // modals
  bsModalRef: BsModalRef;
  subscriptions: Subscription[] = [];

  // pagination || form
  numberRows = 15;
  currentPage = 1;

  /** Search bar - specified text */
  searchText: string;

  constructor(
    private modalService: BsModalService,
    private internalModalService: ModalService,
    private internalFormService: FormService,
    private auctionService: AuctionsService,
    private notificationService: NotificationsService
  ) { }

  ngOnInit(): void {
    this.setupInitialAuctionRequest();
    this.loadAuctions();
  }

  addModal(): void {
    const initialState = {};
    const modalConfig = { ...this.internalModalService.defaultModalOptions, ...{ initialState: initialState, class: 'modal-lg' } };
    this.bsModalRef = this.modalService.show(AuctionAddMainWizardComponent, modalConfig);

    this.subscriptions.push(
      this.modalService.onHidden.subscribe((result: string) => {
        if (this.internalFormService.onModalHide(result, this.subscriptions)) {
          this.loadAuctions();
        }
      })
    );
  }

  editModal(): void {
    const initialState = {
      selectedAuction: this.selected[0]
    };
    console.log("TCL: AdminAuctionMainComponent -> initialState", initialState)

    const modalConfig = { ...this.internalModalService.defaultModalOptions, ...{ initialState: initialState, class: 'modal-lg' } };
    this.bsModalRef = this.modalService.show(AuctionEditComponent, modalConfig);

    this.subscriptions.push(
      this.modalService.onHidden.subscribe((result: string) => {
        if (this.internalFormService.onModalHide(result, this.subscriptions)) {
          this.loadAuctions();
        }
      })
    );
  }

  deleteModal(): void {
    const initialState = {
      selectedAuctions: this.selected
    };

    const modalConfig = { ...this.internalModalService.defaultModalOptions, ...{ initialState: initialState, class: 'modal-lg' } };
    this.bsModalRef = this.modalService.show(AuctionDeleteComponent, modalConfig);

    this.subscriptions.push(
      this.modalService.onHidden.subscribe((result: string) => {
        if (this.internalFormService.onModalHide(result, this.subscriptions)) {
          this.loadAuctions();
        }
      })
    );
  }

  private setupInitialAuctionRequest(): void {
    this.request = {
      auctionStartDate: moment().subtract(365, 'days').format('DD/MM/YYYY'),
      auctionEndDate: moment().format('DD/MM/YYYY'),
      sizeOfPage: this.numberRows,
      currentPage: this.currentPage,
      sortByColumn: 'AuctionName', // by default sort by auction name
      sortingDirection: 'asc', // by default ascending
      searchValue: this.searchText
    };
  }

  /** Gets ALL auctions */
  private loadAuctions(): void {
    this.mainSubscription = this.auctionService
      .getAuctions$(this.request)
      .subscribe(
        (response: AuctionModel) => { this.auctionTable = response; },
        (error: string) => this.notificationService.error(error)
      );
  }
}
