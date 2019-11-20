// angular
import { Component, OnInit } from "@angular/core";

// 3rd lib
import { Subscription } from "rxjs";

// internal
import { AuthService } from "ClientApp/src/app/core/services/auth/auth.service";
import { PermissionService } from "ClientApp/src/app/core/services/permissions/permission.service";
import { NotificationsService } from "ClientApp/src/app/core/services/notifications/notifications.service";
import { PermissionConstants } from "ClientApp/src/app/core/constants/permissions/permission-constants";

@Component({
  selector: "app-navbar",
  templateUrl: "./navbar.component.html"
})
export class NavbarComponent implements OnInit {
  // component
  navBarSub: Subscription;

  userDetails = this.authService.userDetails;

  /** The navbarOpen variable would be set to either true or false , depending if the navbar is open or not, when we click the button to see it */
  navbarOpen = false;

  /** Used to show or hide Admin panel in nav bar */
  canAccessAdminPanel: boolean = false;

  constructor(
    private authService: AuthService,
    private permissionService: PermissionService,
    private notificationService: NotificationsService
  ) {}

  ngOnInit(): void {
    this.handleAdminPanel();
  }

  /** Used to handle sign-out */
  signOut(): void {
    this.authService.logout();
  }

  /** Used to handle responsive show/hide nav bar menu items */
  toggleNavbar(): void {
    this.navbarOpen = !this.navbarOpen;
  }

  /** Used to handle sign-in */
  onSignInChange(): void {
    this.authService.login();
  }

  /**
   * Avoid memory leaks here by cleaning up after ourselves
   */
  ngOnDestroy(): void {
    if (this.navBarSub) {
      this.navBarSub.unsubscribe();
    }
  }

  private handleAdminPanel(): void {
    this.navBarSub = this.permissionService.userPermissions.subscribe(
      permissions => {
        if (permissions != null) {
          this.canAccessAdminPanel = permissions.includes(
            PermissionConstants.CAN_ACCESS_ADMIN_PANEL
          )
            ? true
            : false;
        }
      },
      (error: string) => this.notificationService.error(error)
    );
  }
}
