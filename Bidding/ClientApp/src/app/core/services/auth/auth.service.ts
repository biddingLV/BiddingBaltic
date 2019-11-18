// angular
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

// 3rd lib
import { CookieService } from "ngx-cookie-service";
import { catchError } from "rxjs/operators";
import { Observable } from "rxjs";

// internal
import { ExceptionsService } from "../exceptions/exceptions.service";
import { NotificationsService } from "../notifications/notifications.service";

// internal model
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
    private httpClient: HttpClient,
    private exceptionService: ExceptionsService,
    private notificationService: NotificationsService
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
    if (localStorage.getItem("userPermissions")) {
      localStorage.removeItem("userPermissions");
    }

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

    this.loadUserPermissions$().subscribe(
      (response: string) => {
        localStorage.setItem("userPermissions", response);
      },
      (error: string) => this.notificationService.error(error)
    );
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

  loadUserPermissions$(): Observable<string> {
    const permissionsUrl = "/api/permissions/LoadUserPermissions";

    return this.httpClient
      .get<string>(permissionsUrl)
      .pipe(catchError(this.exceptionService.errorHandler));
  }
}
