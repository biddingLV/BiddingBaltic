// angular
import { Injectable } from "@angular/core";

// 3rd lib
import { CookieService } from "ngx-cookie-service";
import { PermissionService } from "../permissions/permission.service";

// internal
export class User {
  UserId: number;
  IsAuthenticated: boolean;
  FirstName: string;
  LastName: string;
  Email: string;
}

interface TxWindow extends Window {
  global: Window;
}

((window as unknown) as TxWindow).global = window;

@Injectable()
export class AuthService {
  userDetails: User;
  redirectUri: string;

  constructor(
    private cookieService: CookieService,
    private permissionService: PermissionService
  ) {
    this.checkCookie();
  }

  login(): void {
    if (this.redirectUri !== undefined) {
      window.location.href =
        "/start/auth/login?redirectPage=" + this.redirectUri;
    } else {
      window.location.href = "/start/auth/login";
    }
  }

  logout(): void {
    window.location.href = "/start/auth/logout";
  }

  isAuthenticated(): boolean {
    if (this.userDetails == null) {
      this.checkCookie();
    }

    if (this.userDetails != null) {
      return this.userDetails.IsAuthenticated;
    } else {
      return false;
    }
  }

  private checkCookie(): void {
    if (this.cookieService.check("BIDPROFILE")) {
      this.handleProfileCookieExists();
    } else {
      this.userDetails = null;
    }
  }

  private handleProfileCookieExists() {
    const profileCookie = this.cookieService.get("BIDPROFILE");

    this.setUserDetails(profileCookie);
    this.permissionService.setUserPermissions();
  }

  private setUserDetails(profileCookie: string): void {
    const profile = JSON.parse(profileCookie);
    this.userDetails = new User();
    this.userDetails.UserId = profile.UserId;
    this.userDetails.IsAuthenticated = profile.IsAuthenticated;
    this.userDetails.FirstName = profile.FirstName;
    this.userDetails.LastName = profile.LastName;
    this.userDetails.Email = profile.Email;
  }
}
