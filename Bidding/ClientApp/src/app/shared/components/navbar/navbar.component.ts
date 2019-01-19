// angular
import { Component, OnInit } from '@angular/core';

// 3rd lib
import { ScrollToService, ScrollToConfigOptions } from '@nicky-lenaers/ngx-scroll-to';

// internal
import { AuthService } from 'ClientApp/src/app/core/services/auth/auth.service';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  username = '';

  constructor(public auth: AuthService, private scrollToService: ScrollToService) {
    if (auth.userInfo) {
      this.username = this.auth.userInfo.UserName;
    }
  }

  ngOnInit(): void { }

  signIn(): void {
    this.auth.login();
  }

  signOut(): void {
    this.auth.logout();
  }

  triggerScrollTo(): void {
    const config: ScrollToConfigOptions = {
      target: 'destination'
    };

    this.scrollToService.scrollTo(config);
  }
}
