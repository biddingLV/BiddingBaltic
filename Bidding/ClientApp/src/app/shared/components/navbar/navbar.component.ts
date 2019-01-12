import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';

import { ScrollToService, ScrollToConfigOptions } from '@nicky-lenaers/ngx-scroll-to';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  public username = '';
  public isAuthenticated = false;

  constructor(private router: Router, private _scrollToService: ScrollToService) {
    router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        // this.isAuthenticated = this.authService.isAuthenticated();
        this.username = localStorage.getItem('username');
      }
    });
  }

  public ngOnInit(): void { }

  public triggerScrollTo() {

    const config: ScrollToConfigOptions = {
      target: 'destination'
    };

    this._scrollToService.scrollTo(config);
  }
}
