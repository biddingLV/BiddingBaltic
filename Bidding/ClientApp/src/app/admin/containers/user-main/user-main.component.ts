// angular
import { Component, OnInit } from '@angular/core';

// 3rd lib
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';

// internal
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';
import { UserListRequestModel } from '../../models/list/user-list-request.model';
import { UserListResponseModel } from '../../models/list/user-list-response.model';
import { UserListItemModel } from '../../models/list/user-list-item.model';
import { UserEditComponent } from 'ClientApp/src/app/users/components/edit/edit.component';
import { UsersService } from 'ClientApp/src/app/users/services/users.service';
import { FormService } from 'ClientApp/src/app/core/services/form/form.service';
import { ModalService } from 'ClientApp/src/app/core/services/modal/modal.service';


@Component({
  templateUrl: './user-main.component.html'
})
export class AdminUserMainComponent implements OnInit {
  // Component
  mainSubscription: Subscription;

  // API
  userTable: UserListResponseModel;
  request: UserListRequestModel;

  // table
  selected: UserListItemModel[] = [];

  // modals
  bsModalRef: BsModalRef;
  subscriptions: Subscription[] = [];

  // pagination || form
  numberRows = 15;
  currentPage = 1;

  /** Search bar - specified text */
  searchText: string;

  constructor(
    private externalModalService: BsModalService,
    private internalModalService: ModalService,
    private internalFormService: FormService,
    private usersService: UsersService,
    private notificationService: NotificationsService
  ) { }

  ngOnInit(): void {
    this.setupInitialAuctionRequest();
    this.loadUsers();
  }

  // addModal(): void {

  // }

  editModal(): void {
    const initialState = {
      selectedUserId: this.selected[0].userId
    };

    const modalConfig = { ...this.internalModalService.defaultModalOptions, ...{ initialState: initialState, class: 'modal-md' } };
    this.bsModalRef = this.externalModalService.show(UserEditComponent, modalConfig);

    this.subscriptions.push(
      this.externalModalService.onHidden.subscribe((result: string) => {
        if (this.internalFormService.onModalHide(result, this.subscriptions)) {
          this.loadUsers();
        }
      })
    );
  }

  // deleteModal(): void {

  // }

  private setupInitialAuctionRequest(): void {
    this.request = {
      offsetStart: 0,
      offsetEnd: this.numberRows,
      currentPage: this.currentPage,
      sortByColumn: 'FirstName',
      sortingDirection: 'asc',
      searchValue: this.searchText
    };
  }

  /** Gets all users */
  private loadUsers(): void {
    this.mainSubscription = this.usersService
      .getUsers$(this.request)
      .subscribe(
        (response: UserListResponseModel) => { this.userTable = response; },
        (error: string) => this.notificationService.error(error)
      );
  }
}
