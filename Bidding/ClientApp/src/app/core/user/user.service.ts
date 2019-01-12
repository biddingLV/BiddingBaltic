// Based on: https://auth0.com/docs/quickstart/spa/angular2/01-login

import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { Subscription } from 'rxjs/Subscription';
import { Profile } from '../../models/profile.model';
import { environment } from '../../../../environments/environment';
import { CookieService } from 'ngx-cookie-service';
import { User } from '../../models/user.model';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/mergeMap';

interface TxWindow extends Window {
  global: Window;
}

(window as TxWindow).global = window;

@Injectable()
export class UserService {
  public userProfile: Profile;
  public userInfo: User;

  constructor(public router: Router, private cookieService: CookieService) {
    this.checkCookie();
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

  public login(): void {
    window.location.href = environment.authLoginUri;
  }

  public logout(): void {
    window.location.href = environment.authLogoutUri;
  }

  public isAuthenticated(): boolean {
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
}
