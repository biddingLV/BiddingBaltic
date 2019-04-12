// Based on: https://auth0.com/docs/quickstart/spa/angular2/01-login
// angular
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

// 3rd lib
import { CookieService } from 'ngx-cookie-service/cookie-service/cookie.service';

// internal
import { environment } from '../../../../environments/environment';
import { User } from '../../models/user/user.model';


interface TxWindow extends Window {
  global: Window;
}

(window as TxWindow).global = window;

@Injectable()
export class AuthService {
  userDetails: User;
  redirectUri: string;

  constructor(
    public router: Router,
    private cookieService: CookieService
  ) {
    this.checkCookie();
  }

  login(): void {
    if (this.redirectUri !== undefined) {
      window.location.href = environment.authLoginUri + '?redirectPage=' + this.redirectUri;
    } else {
      window.location.href = environment.authLoginUri;
    }
  }

  logout(): void {
    window.location.href = environment.authLogoutUri;
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
    if (this.cookieService.check('BIDPROFILE')) {
      const profileCookie = this.cookieService.get('BIDPROFILE');
      if (profileCookie) {
        this.setUserDetails(profileCookie);
      }
    } else {
      this.userDetails = null;
    }
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
