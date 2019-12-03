// angular
import { Component, OnInit } from "@angular/core";

// 3rd lib
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { Subscription } from "rxjs";

// internal
import { NotificationsService } from "ClientApp/src/app/core/services/notifications/notifications.service";
import { UserListRequestModel } from "../../models/list/user-list-request.model";
import { UserListResponseModel } from "../../models/list/user-list-response.model";
import { UserListItemModel } from "../../models/list/user-list-item.model";
import { UsersService } from "ClientApp/src/app/users/services/users.service";
import { FormService } from "ClientApp/src/app/core/services/form/form.service";
import { ModalService } from "ClientApp/src/app/core/services/modal/modal.service";
import { UserAdvancedEditComponent } from "ClientApp/src/app/users/components/advanced-edit/advanced-edit.component";

@Component({
  templateUrl: "./user-main.component.html"
})
export class AdminUserMainComponent implements OnInit {
  // Component
  mainSubscription: Subscription;

  // API
  userTable: UserListResponseModel;
  requestModel: UserListRequestModel;

  // table
  selected: UserListItemModel[] = [];

  // modals
  bsModalRef: BsModalRef;

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
  ) {}

  ngOnInit(): void {
    this.setupInitialAuctionRequest();
    this.loadUsers();
  }

  editModal(): void {
    const initialState = {
      selectedUserId: this.selected[0].userId
    };

    const modalConfig = {
      ...this.internalModalService.defaultModalOptions,
      ...{ initialState: initialState }
    };

    this.bsModalRef = this.externalModalService.show(
      UserAdvancedEditComponent,
      modalConfig
    );

    this.externalModalService.onHide
      .pipe(this.internalModalService.toModalResult())
      .subscribe(result => {
        if (result.success) {
          this.loadUsers();
        }
      });
  }

  private setupInitialAuctionRequest(): void {
    this.requestModel = {
      offsetStart: 0,
      offsetEnd: this.numberRows,
      currentPage: this.currentPage,
      sortByColumn: "FirstName",
      sortingDirection: "asc",
      searchValue: this.searchText
    };
  }

  /** Gets all users */
  private loadUsers(): void {
    this.mainSubscription = this.usersService
      .getUsers$(this.requestModel)
      .subscribe(
        (response: UserListResponseModel) => {
          this.userTable = response;
        },
        (error: string) => this.notificationService.error(error)
      );
  }
}
