// angular
import { Component, OnInit } from '@angular/core';

// 3rd lib
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';

// internal
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';
import { ModalService, FormService } from 'ClientApp/src/app/core';
import { UserService } from '../../services/user.service';


@Component({
  templateUrl: './user-main.component.html'
})
export class AdminUserMainComponent implements OnInit {
  // Component
  mainSubscription: Subscription;

  // API
  userTable: any;// AuctionModel; // todo: kke: specify type!
  request: any;// AuctionListRequest; // todo: kke: specify type!

  // table
  selected = [];// : AuctionItemModel[] = []; // todo: kke: specify type!

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
    private userService: UserService,
    private notificationService: NotificationsService
  ) { }

  ngOnInit(): void {
    this.setupInitialAuctionRequest();
    this.loadUsers();
  }

  addModal(): void {

  }

  editModal(): void {
    // const initialState = {
    //   selectedAuctionId: this.selected[0].auctionId
    // };

    // const modalConfig = { ...this.internalModalService.defaultModalOptions, ...{ initialState: initialState, class: 'modal-lg' } };
    // this.bsModalRef = this.modalService.show(AuctionEditComponent, modalConfig);

    // this.subscriptions.push(
    //   this.modalService.onHidden.subscribe((result: string) => {
    //     if (this.internalFormService.onModalHide(result, this.subscriptions)) {
    //       this.loadAuctions();
    //     }
    //   })
    // );
  }

  deleteModal(): void {

  }

  private setupInitialAuctionRequest(): void {
    this.request = {
      sizeOfPage: this.numberRows,
      currentPage: this.currentPage,
      sortByColumn: 'FirstName',
      sortingDirection: 'asc',
      searchValue: this.searchText
    };
  }

  /** Gets ALL users */
  private loadUsers(): void {
    this.mainSubscription = this.userService
      .getUsers$(this.request)
      .subscribe(
        (response: any) => { this.userTable = response; },
        (error: string) => this.notificationService.error(error)
      );
  }
}
