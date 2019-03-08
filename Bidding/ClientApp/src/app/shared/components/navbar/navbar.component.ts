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
  fullName: string;
  userDetails = this.auth.userDetails;

  constructor(
    private auth: AuthService,
    private scrollToService: ScrollToService
  ) {

  }

  ngOnInit(): void {
    this.setFullName();
  }

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

  /**
   * Sets users full name.
   * If first & last name exists then show full name.
   * If first & last name doesnt exists show email as full name.
   */
  private setFullName(): void {
    if (this.userDetails.FirstName && this.userDetails.LastName) {
      this.fullName = this.userDetails.FirstName + ' ' + this.userDetails.LastName
    } else {
      this.fullName = this.userDetails.Email
    }
  }
}
