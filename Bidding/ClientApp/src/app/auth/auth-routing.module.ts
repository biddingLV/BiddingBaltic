// angular
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// internal - services
import { NoAuthGuard } from '../core/services/auth/no-auth-guard.service';

// internal - components
import { SignInComponent } from './components/sign-in/sign-in.component';

const routes: Routes = [
  {
    path: 'sign-in',
    canActivate: [NoAuthGuard],
    component: SignInComponent,
    data: { breadcrumb: 'Sign-In', hideBreadcrumb: true, title: 'Sign-In', hideHeader: true, hideFooter: true }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule { }
