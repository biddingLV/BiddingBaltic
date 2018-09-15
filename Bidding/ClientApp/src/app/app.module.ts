import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

// Style Dependencies
import { ModalModule } from 'ngx-bootstrap/modal';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
// import { PingComponent } from './ping/ping.component';
// import { ProfileComponent } from './profile/profile.component';
// import { AdminComponent } from './admin/admin.component';

import { AuthService } from './auth/auth.service';
import { AuthGuardService } from './auth/auth-guard.service';
import { ScopeGuardService } from './auth/scope-guard.service';

// shared
import { SharedModule } from './shared/shared.module';
import { SubscribeEmailComponent } from './home/components/coming-soon/subscribe/email/email.component';
import { HomeModule } from './home/home.module';
import { SubscribeWhatsappComponent } from './home/components/coming-soon/subscribe/whatsapp/whatsapp.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    ModalModule.forRoot(),
    AppRoutingModule,
    SharedModule,
    HomeModule
  ],
  providers: [
    AuthService,
    AuthGuardService,
    ScopeGuardService
  ],
  bootstrap: [
    AppComponent
  ],
  entryComponents: [
    SubscribeEmailComponent,
    SubscribeWhatsappComponent
  ]
})
export class AppModule { }
