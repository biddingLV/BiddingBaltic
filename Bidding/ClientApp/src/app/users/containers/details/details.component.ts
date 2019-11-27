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
import { UserDetailsModel } from "../../models/details/user-details.model";
import { UserEditComponent } from "../../components/edit/edit.component";
import { FormService } from "ClientApp/src/app/core/services/form/form.service";
import { ModalService } from "ClientApp/src/app/core/services/modal/modal.service";
import { BreadcrumbItem } from "ClientApp/src/app/shared/models/breadcrumb-item.model";

@Component({
  selector: "app-user-details",
  templateUrl: "./details.component.html",
  styleUrls: ["./details.component.scss"]
})
export class UserDetailsComponent implements OnInit {
  // details
  detailsSub: Subscription;
  userDetails: UserDetailsModel;

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
    private internalModalService: ModalService,
    private internalFormService: FormService
  ) {}

  ngOnInit(): void {
    this.loadUserDetails();
  }

  // editModal(): void {
  //   const initialState = {
  //     selectedUserId: this.userDetails.userId
  //   };

  //   const modalConfig = {
  //     ...this.internalModalService.defaultModalOptions,
  //     ...{ initialState: initialState, class: "modal-md" }
  //   };
  //   this.bsModalRef = this.externalModalService.show(
  //     UserEditComponent,
  //     modalConfig
  //   );

  //   this.subscriptions.push(
  //     this.externalModalService.onHidden.subscribe((result: string) => {
  //       if (this.internalFormService.onModalHide(result, this.subscriptions)) {
  //         this.loadUserDetails();
  //       }
  //     })
  //   );
  // }

  /**
   * Load user information/details for a specific user
   */
  private loadUserDetails() {
    this.detailsSub = this.activedRoute.paramMap
      .pipe(
        switchMap((params: ParamMap) =>
          this.userService.getUserDetails$(Number(params.get("id")))
        )
      )
      .subscribe(
        response => {
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
    if (this.userDetails.userFirstName && this.userDetails.userLastName) {
      this.fullName =
        this.userDetails.userFirstName + " " + this.userDetails.userLastName;
    } else {
      this.fullName = this.userDetails.userEmail;
    }
  }
}
