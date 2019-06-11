// angular
import { Component, OnInit } from '@angular/core';

// 3rd lib
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';

// internal
import { AuctionEditComponent } from 'ClientApp/src/app/auctions/components/edit/edit.component';
import { AuctionAddMainWizardComponent } from 'ClientApp/src/app/auctions/containers/wizard/main/main.component';
import { AuctionDeleteComponent } from 'ClientApp/src/app/auctions/components/delete/delete.component';
import { ModalService } from 'ClientApp/src/app/core/services/modal/modal.service';
import { FormService } from 'ClientApp/src/app/core/services/form/form.service';


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
  subscriptions: Subscription[] = [];

  // list
  updateAuctionList: boolean = false; // todo: kke: this is not wotking atm!

  constructor(
    private modalService: BsModalService,
    private internalModalService: ModalService,
    private internalFormService: FormService
  ) { }

  ngOnInit(): void { }

  addModal(): void {
    const initialState = {};
    const modalConfig = { ...this.internalModalService.defaultModalOptions, ...{ initialState: initialState, class: 'modal-lg' } };
    this.bsModalRef = this.modalService.show(AuctionAddMainWizardComponent, modalConfig);

    this.subscriptions.push(
      this.modalService.onHidden.subscribe((result: string) => {
        if (this.internalFormService.onModalHide(result, this.subscriptions)) {
          this.updateAuctionList = true;
        }
      })
    );
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
