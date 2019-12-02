// angular
import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, ParamMap } from "@angular/router";

// 3rd lib
import { switchMap } from "rxjs/operators";
import { Subscription } from "rxjs";
import { BsModalService, BsModalRef } from "ngx-bootstrap/modal";

// internal
import { UsersService } from "../../services/users.service";
import { NotificationsService } from "ClientApp/src/app/core/services/notifications/notifications.service";
import { UserDetailsResponseModel } from "../../models/details/user-details-response.model";
import { UserEditComponent } from "../../components/edit/edit.component";
import { ModalService } from "ClientApp/src/app/core/services/modal/modal.service";
import { BreadcrumbItem } from "ClientApp/src/app/shared/models/breadcrumb-item.model";

@Component({
  selector: "app-user-details",
  templateUrl: "./details.component.html",
  styleUrls: ["./details.component.scss"]
})
export class UserDetailsComponent implements OnInit {
  // component
  detailsSub: Subscription;
  userDetails: UserDetailsResponseModel;
  private userId: number;

  // template
  fullName: string;

  // modals
  bsModalRef: BsModalRef;
  subscriptions: Subscription[] = [];

  // breadcrumbs
  breadcrumbs: BreadcrumbItem[];

  constructor(
    private userService: UsersService,
    private activedRoute: ActivatedRoute,
    private notificationService: NotificationsService,
    private externalModalService: BsModalService,
    private internalModalService: ModalService
  ) {}

  ngOnInit(): void {
    this.loadUserDetails();
  }

  editModal(): void {
    const initialState = {
      selectedUserId: this.userId
    };

    const modalConfig = {
      ...this.internalModalService.defaultModalOptions,
      ...{ initialState: initialState, class: "modal-md" }
    };

    this.bsModalRef = this.externalModalService.show(
      UserEditComponent,
      modalConfig
    );

    this.externalModalService.onHide
      .pipe(this.internalModalService.toModalResult())
      .subscribe(result => {
        if (result.success) {
          this.loadUserDetails();
        }
      });
  }

  /**
   * Load user information/details for a specific user
   */
  private loadUserDetails() {
    this.detailsSub = this.activedRoute.paramMap
      .pipe(
        switchMap((params: ParamMap) => {
          this.userId = Number(params.get("id"));
          return this.userService.getUserDetails$(this.userId);
        })
      )
      .subscribe(
        (response: UserDetailsResponseModel) => {
          this.userDetails = response;
          this.generateBreadcrumbs();
          this.setFullName();
        },
        (error: string) => this.notificationService.error(error)
      );
  }

  private generateBreadcrumbs() {
    this.breadcrumbs = [
      {
        name: "Sākumlapa",
        url: "/"
      },
      {
        name: "Mans abonoments",
        url: ""
      }
    ];
  }

  private setFullName(): void {
    if (this.userDetails.firstName && this.userDetails.lastName) {
      this.fullName =
        this.userDetails.firstName + " " + this.userDetails.lastName;
    } else {
      this.fullName = this.userDetails.email;
    }
  }
}
