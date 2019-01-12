// angular
import { Component } from '@angular/core';
import { Router } from '@angular/router';

// internal
import { AuthService } from 'ClientApp/src/app/core/services/auth/auth.service';

@Component({
  templateUrl: './sign-in.component.html',
  styleUrls: []
})
export class SignInComponent {

  constructor(
    public auth: AuthService,
    public router: Router
  ) { }

  signIn(): void {
    this.auth.login();
    this.router.navigate(['/']);
  }

  signOut(): void {
    this.auth.logout();
    this.router.navigate(['/sign-in']);
  }
}
