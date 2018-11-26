// angular
import { Component, OnInit, OnDestroy, AfterViewInit } from '@angular/core';

// 3rd party libraries
import { Subscription } from 'rxjs';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

// internal
// models & services
import { NotificationsService } from 'src/app/core/services/notifications/notifications.service';
import { AuctionModel } from 'src/app/auctions/models/list/auction.model';
import { AuctionsService } from 'src/app/auctions/services/auctions.service';
import { AuctionListRequest } from 'src/app/auctions/models/list/auction-list-request.model';
import { CategoryModel } from 'src/app/auctions/models/filters/category.model';

// components
import { AuctionEditComponent } from '../../components/edit/edit.component';
import { AuctionAddComponent } from '../../components/add/add.component';
import { AuctionDeleteComponent } from '../../components/delete/delete.component';


@Component({
  // could be a admin panel!
  selector: 'app-admin-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class AdminListComponent implements OnInit, OnDestroy, AfterViewInit {
  // table
  auctionsSub: Subscription;
  adminTable: AuctionModel;

  // pagination
  numberRows = 10;

  //filters
  categories: CategoryModel[];

  //utility
  loading: boolean;

  // API
  request: AuctionListRequest;

  // modals
  bsModalRef: BsModalRef;

  constructor(
    private auctionApi: AuctionsService,
    private notification: NotificationsService,
    private modalService: BsModalService
  ) { }

  ngOnInit() {
    this.setupRequest();
    this.getAuctionList();
  }

  // Request Update Events
  updateRequest(property: string, event) {
    if (property === 'Page') {
      this.request.currentPage = event.page;
    } else {
      this.request[property] = (event === undefined) || (event === 'undefined') ? '' : event;
      this.request.currentPage = 1;
    }

    this.getAuctionList();
  }

  // Sort Update Events
  onSortChange(event): void {
    this.request.sortingDirection = this.request.sortByColumn === event.column.prop ?
      this.request.sortingDirection === 'asc' ? 'desc' : 'asc'
      : 'asc'; // TODO: HS: maybe this can still be new value
    this.request.sortByColumn = event.column.prop;
    this.request.currentPage = 1;

    this.getAuctionList();
  }

  ngOnDestroy() {
    this.auctionsSub.unsubscribe();
  }

  ngAfterViewInit() {
    // this.loadCategoryFilter();
  }

  // Modals
  editModal() {
    const initialState = {
      // userId: this.userDetails.Id,
      // organizationId: this.userDetails.OrgId,
      // organizationName: this.userDetails.OrganizationName,
      // signInEmail: this.userDetails.LoginEmail,
      // roleId: this.userDetails.RoleId,
      // roleName: this.userDetails.RoleName,
      // fullName: this.userDetails.UserName,
      // firstName: this.userDetails.FirstName,
      // lastName: this.userDetails.LastName,
      // phone: this.userDetails.Phone
    };

    this.bsModalRef = this.modalService.show(AuctionEditComponent, { initialState });
    this.bsModalRef.content.closeBtnName = 'Close';
    this.modalService.onHide.subscribe(resp => {

    });
  }

  deleteModal() {
    const initialState = {
      // userId: this.userDetails.Id,
      // organizationId: this.userDetails.OrgId,
      // organizationName: this.userDetails.OrganizationName,
      // signInEmail: this.userDetails.LoginEmail,
      // roleId: this.userDetails.RoleId,
      // roleName: this.userDetails.RoleName,
      // fullName: this.userDetails.UserName,
      // firstName: this.userDetails.FirstName,
      // lastName: this.userDetails.LastName,
      // phone: this.userDetails.Phone
    };

    this.bsModalRef = this.modalService.show(AuctionDeleteComponent, { initialState });
    this.bsModalRef.content.closeBtnName = 'Close';
    this.modalService.onHide.subscribe(resp => {

    });
  }

  addModal() {
    const initialState = {
      // userId: this.userDetails.Id,
      // organizationId: this.userDetails.OrgId,
      // organizationName: this.userDetails.OrganizationName,
      // signInEmail: this.userDetails.LoginEmail,
      // roleId: this.userDetails.RoleId,
      // roleName: this.userDetails.RoleName,
      // fullName: this.userDetails.UserName,
      // firstName: this.userDetails.FirstName,
      // lastName: this.userDetails.LastName,
      // phone: this.userDetails.Phone
    };

    this.bsModalRef = this.modalService.show(AuctionAddComponent, { initialState });
    this.bsModalRef.content.closeBtnName = 'Close';
    this.modalService.onHide.subscribe(resp => {

    });
  }

  private setupRequest(): void {
    // todo: kke: improve this
    this.request = {
      starDate: new Date(),
      endDate: new Date(),
      sizeOfPage: this.numberRows,
      currentPage: 1,
      sortByColumn: 'Name',
      sortingDirection: 'asc',
      searchValue: ''
    };
  }

  private getAuctionList() {
    // this.loading = true;
    console.log('request', this.request)
    // Get all (admin) events
    // this.auctionsSub = this.auctionApi
    //   .getAuctions$(this.request)
    //   .subscribe(
    //     (result: AuctionModel) => { this.adminTable = result; },
    //     (error: string) => this.notification.error(error)
    //   );
  }

  private loadCategoryFilter() {
    // get all categories for the filter
    this.auctionsSub = this.auctionApi
      .getCategories$()
      .subscribe(
        (result: CategoryModel[]) => { this.categories = result; },
        (error: string) => this.notification.error(error)
      );
  }
}
