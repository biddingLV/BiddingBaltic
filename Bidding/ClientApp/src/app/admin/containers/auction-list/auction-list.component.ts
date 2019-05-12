// angular
import { Component, OnInit } from '@angular/core';

// 3rd lib
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

// internal
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';
import { AuctionEditComponent } from 'ClientApp/src/app/auctions/components/edit/edit.component';
import { AuctionAddMainWizardComponent } from 'ClientApp/src/app/auctions/containers/wizard/main/main.component';


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
    private notification: NotificationsService,
    private modalService: BsModalService
  ) { }

  ngOnInit() {

  }

  // Modals
  editModal() {

    const initialState = {
      auctionName: this.selected[0].name,
      auctionPrice: this.selected[0].price,
      auctionStartDate: this.selected[0].startDate,
      auctionEndDate: this.selected[0].endDate,
      auctionDescription: this.selected[0].description
    };

    this.bsModalRef = this.modalService.show(AuctionEditComponent, { initialState });
    this.bsModalRef.content.closeBtnName = 'Close';
    this.modalService.onHide.subscribe(() => { }); // this.getAuctions();
  }

  addModal() {
    const initialState = {};
    this.bsModalRef = this.modalService.show(AuctionAddMainWizardComponent, { initialState, class: 'modal-lg' });
    this.bsModalRef.content.closeBtnName = 'Close';
    this.modalService.onHide.subscribe(() => { }); // this.getAuctions();
  }

  deleteModal() {
    const initialState = {
      // userId: this.selected[0].Id,
      // fullName: this.selected[0].FullName,
      // isContactPerson: this.selected[0].ContactPerson,
      // organizationName: this.selected[0].CompanyName,
      // organizationId: this.selected[0].OrgId
    };

    // this.bsModalRef = this.modalService.show(UsersDeleteComponent, { initialState });
    // this.bsModalRef.content.closeBtnName = 'Close';
    // this.selected = [];
    // this.modalService.onHide.subscribe(() => { this.getUsers(); });
  }
}
