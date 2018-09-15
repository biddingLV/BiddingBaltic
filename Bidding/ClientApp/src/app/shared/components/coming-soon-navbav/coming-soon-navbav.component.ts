import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';

import { AuthService } from '../../../auth/auth.service';

@Component({
  selector: 'app-coming-soon-navbav',
  templateUrl: './coming-soon-navbav.component.html',
  styleUrls: ['./coming-soon-navbav.component.scss']
})
export class ComingSoonNavbarComponent implements OnInit {
  public username = '';
  public isAuthenticated = false;

  constructor(private router: Router, private authService: AuthService) {
    router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.isAuthenticated = this.authService.isAuthenticated();
        this.username = localStorage.getItem('username');
      }
    });
  }

  public ngOnInit(): void { }

  private login(): void {
    this.authService.login();
  }

  private logout(): void {
    this.authService.logout();
  }
}
