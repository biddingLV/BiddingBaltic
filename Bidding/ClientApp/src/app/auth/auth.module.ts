// Angular
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// internal
import { MinSharedModule } from '../shared/shared-min.module';
import { AuthRoutingModule } from './auth-routing.module';
import { NoAuthGuard } from '../core/services/auth/no-auth-guard.service';
import { PageNotFoundComponent } from './components/404-page-not-found/404-page-not-found.component';


@NgModule({
  imports: [
    CommonModule,
    MinSharedModule,
    AuthRoutingModule
  ],
  declarations: [
    PageNotFoundComponent
  ],
  providers: [
    NoAuthGuard
  ]
})
export class AuthModule { }
