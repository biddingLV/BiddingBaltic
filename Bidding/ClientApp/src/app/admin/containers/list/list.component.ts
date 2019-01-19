// angular
import { Component, OnInit } from '@angular/core';

// 3rd lib
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

// internal
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';
import { AuctionAddComponent } from 'ClientApp/src/app/auctions/components/add/add.component';


@Component({
  templateUrl: './list.component.html',
  styleUrls: []
})
export class AdminListComponent implements OnInit {
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
      // userId: this.selected[0].Id,
      // organizationId: this.selected[0].OrgId,
      // organizationName: this.selected[0].CompanyName,
      // signInEmail: this.selected[0].LoginEmail,
      // roleId: this.selected[0].RoleId,
      // roleName: this.selected[0].RoleName,
      // fullName: this.selected[0].FullName,
      // firstName: this.selected[0].FirstName,
      // lastName: this.selected[0].LastName
    };

    // this.bsModalRef = this.modalService.show(UsersEditComponent, { initialState });
    // this.bsModalRef.content.closeBtnName = 'Close';
    // this.selected = [];
    // this.modalService.onHide.subscribe(() => { this.getUsers(); });
  }

  addModal() {
    const initialState = {};
    this.bsModalRef = this.modalService.show(AuctionAddComponent, { initialState });
    // this.bsModalRef.content.closeBtnName = 'Close';
    // this.modalService.onHide.subscribe(() => { this.getUsers(); });
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
