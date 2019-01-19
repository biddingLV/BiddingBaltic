// Angular
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// internal - modules
import { MinSharedModule } from '../shared/shared-min.module';

// internal - Routing
import { AuthRoutingModule } from './auth-routing.module';

// internal - Services
import { NoAuthGuard } from '../core/services/auth/no-auth-guard.service';

// internal - Components
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
