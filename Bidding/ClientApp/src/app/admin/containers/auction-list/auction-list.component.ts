// angular
import { Component, OnInit } from '@angular/core';

// 3rd lib
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

// internal
import { AuctionEditComponent } from 'ClientApp/src/app/auctions/components/edit/edit.component';
import { AuctionAddMainWizardComponent } from 'ClientApp/src/app/auctions/containers/wizard/main/main.component';
import { AuctionDeleteComponent } from 'ClientApp/src/app/auctions/components/delete/delete.component';
import { ModalService } from 'ClientApp/src/app/core/services/modal/modal.service';


@Component({
  selector: 'app-admin-auction-list',
  templateUrl: './auction-list.component.html',
  styleUrls: []
})
export class AdminAuctionListComponent implements OnInit {
  // table
  selected = [];

  // modals
  bsModalRef: BsModalRef;

  constructor(
    private modalService: BsModalService,
    private internalModalService: ModalService
  ) { }

  ngOnInit(): void { }

  addModal(): void {
    const initialState = {};
    const modalConfig = { ...this.internalModalService.defaultModalOptions, ...{ initialState: initialState, class: 'modal-lg' } };
    this.bsModalRef = this.modalService.show(AuctionAddMainWizardComponent, modalConfig);
    // todo: kke: add subscription magic!
  }

  editModal(): void {
    const initialState = {
      selectedAuction: this.selected[0]
    };

    const modalConfig = { ...this.internalModalService.defaultModalOptions, ...{ initialState: initialState, class: 'modal-lg' } };
    this.bsModalRef = this.modalService.show(AuctionEditComponent, modalConfig);

    // todo: kke: add subscription magic!
  }

  deleteModal(): void {
    const initialState = {
      selectedAuctions: this.selected
    };

    const modalConfig = { ...this.internalModalService.defaultModalOptions, ...{ initialState: initialState, class: 'modal-lg' } };
    this.bsModalRef = this.modalService.show(AuctionDeleteComponent, modalConfig);

    // todo: kke: add subscription magic!
  }
}
