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

// modals
import { SubscribeEmailComponent } from './home/components/coming-soon/subscribe/email/email.component';
import { SubscribeWhatsappComponent } from './home/components/coming-soon/subscribe/whatsapp/whatsapp.component';

// Modules
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { HomeModule } from './home/home.module';
import { BlogModule } from './blog/blog.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    ModalModule.forRoot(),
    SharedModule,
    HomeModule,
    BlogModule,
    CoreModule.forRoot(),
    AppRoutingModule
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
