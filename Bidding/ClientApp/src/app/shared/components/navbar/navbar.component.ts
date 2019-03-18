// angular
import { Component, OnInit } from '@angular/core';

// 3rd lib
import { ScrollToService, ScrollToConfigOptions } from '@nicky-lenaers/ngx-scroll-to';

// internal
import { AuthService } from 'ClientApp/src/app/core/services/auth/auth.service';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html'
})
export class NavbarComponent implements OnInit {
  fullName: string;
  userDetails = this.auth.userDetails;

  /** The navbarOpen variable would be set to either true or false , depending if the navbar is open or not, when we click the button to see it */
  navbarOpen = false;

  constructor(
    private auth: AuthService,
    private scrollToService: ScrollToService
  ) { }

  ngOnInit(): void {
    this.setNavBarUserInformation();
  }

  /** Used to handle sign-in */
  signIn(): void {
    this.auth.login();
  }

  /** Used to handle sign-out */
  signOut(): void {
    this.auth.logout();
  }

  triggerScrollTo(): void {
    const config: ScrollToConfigOptions = {
      target: 'destination'
    };

    this.scrollToService.scrollTo(config);
  }

  /** Used to handle responsive show/hide nav bar menu items */
  toggleNavbar() {
    this.navbarOpen = !this.navbarOpen;
  }

  /**
   * Sets users full name.
   * If first & last name exists then show full name.
   * If first & last name doesnt exists show email as full name.
   */
  private setNavBarUserInformation(): void {
    if (this.userDetails) {
      if (this.userDetails.FirstName && this.userDetails.LastName) {
        this.fullName = this.userDetails.FirstName + ' ' + this.userDetails.LastName
      } else {
        this.fullName = this.userDetails.Email
      }
    }
  }
}
