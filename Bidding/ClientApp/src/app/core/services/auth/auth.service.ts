// Based on: https://auth0.com/docs/quickstart/spa/angular2/01-login
// angular
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

// 3rd lib
import { CookieService } from 'ngx-cookie-service/cookie-service/cookie.service';

// internal
import { environment } from '../../../../environments/environment';
import { User } from '../../models/user.model';


interface TxWindow extends Window {
  global: Window;
}

(window as TxWindow).global = window;

@Injectable()
export class AuthService {
  userInfo: User;
  redirectUri: string;

  constructor(public router: Router, private cookieService: CookieService) {
    this.checkCookie();
  }

  login(): void {
    // TODO: MJU: Better way to do this?
    if (this.redirectUri !== undefined) {   // TODO: MJU: Better way to check null?
      window.location.href = environment.authLoginUri + '?redirectPage=' + this.redirectUri;
    } else {
      window.location.href = environment.authLoginUri;
    }
  }

  logout(): void {
    window.location.href = environment.authLogoutUri;
  }

  isAuthenticated(): boolean {
    if (this.userInfo == null) {    // TODO: MJU: Best way to check for null?
      this.checkCookie();
    }
    // TODO: MJU: Add session expiration to profile cookie.
    if (this.userInfo != null) {
      return this.userInfo.IsAuthenticated;
    } else {
      return false;
    }
  }

  private checkCookie(): void {
    if (this.cookieService.check('TXPROFILE')) {
      const profileCookie = this.cookieService.get('TXPROFILE');
      if (profileCookie !== undefined) {
        const profile = JSON.parse(profileCookie);
        this.userInfo = new User();
        this.userInfo.IsAuthenticated = profile.IsAuthenticated;
        this.userInfo.OrganizationId = profile.OrganizationId;
        this.userInfo.UserId = profile.UserId;
        this.userInfo.UserName = profile.UserName;
      }
    } else {
      this.userInfo = null;
    }
  }
}
