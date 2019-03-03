// angular
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

// 3rd lib
import { switchMap } from 'rxjs/operators';
import { Subscription } from 'rxjs';

// internal
import { UsersService } from '../../services/users.service';
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';
import { UserDetailsModel } from '../../models/details/user-details.model';


@Component({
  selector: 'app-user-details',
  templateUrl: './details.component.html'
})
export class UserDetailsComponent implements OnInit {
  // details
  detailsSub: Subscription;
  userDetails: UserDetailsModel;

  // template
  fullName: string;

  constructor(
    private userApi: UsersService,
    private activedRoute: ActivatedRoute,
    private notification: NotificationsService,
  ) { }

  ngOnInit() {
    this.loadUserDetails();
  }

  editModal(): void {
    const initialState = {
      userId: this.userDetails.userId,
      userFirstName: this.userDetails.userFirstName,
      userLastName: this.userDetails.userLastName
    };

    // this.bsModalRef = this.modalService.show(UsersEditComponent, { initialState });
    // this.bsModalRef.content.closeBtnName = 'Close';
    // // TODO: MJU: Only reload if success.
    // this.modalService.onHide.subscribe(() => {
    //   this.loadUserInformation();
    //   this.getActivities();
    // });
  }

  /**
   * Load user information/details for a specific user
   */
  private loadUserDetails() {
    this.detailsSub =
      this.activedRoute.paramMap.pipe(
        switchMap((params: ParamMap) => this.userApi.getUserDetails$(Number(params.get('id'))))
      ).subscribe(
        response => {
          this.userDetails = response;
          this.setFullName();
        },
        (error: string) => this.notification.error(error)
      );
  }

  private setFullName(): void {
    if (this.userDetails.userFirstName && this.userDetails.userLastName) {
      this.fullName = this.userDetails.userFirstName + ' ' + this.userDetails.userLastName
    } else {
      this.fullName = this.userDetails.userEmail
    }
  }
}
