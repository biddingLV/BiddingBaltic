// angular modules
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// routing
import { AppRoutingModule } from './app-routing.module';

// In-house components
import { SubscribeEmailComponent } from './home/components/coming-soon/subscribe/email/email.component';
import { SubscribeWhatsappComponent } from './home/components/coming-soon/subscribe/whatsapp/whatsapp.component';
import { AppComponent } from './app.component';

// In-house Modules
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { HomeModule } from './home/home.module';
import { AuctionsModule } from './auctions/auctions.module';

// 3rd party modules
import { ScrollToModule } from '@nicky-lenaers/ngx-scroll-to';
import { ModalModule } from 'ngx-bootstrap/modal';

// Services
import { PreviousRouteService } from './shared/services/previous-route.service.ts/previous-route.service';
import { PageNotFoundComponent } from './auth/components/404-page-not-found/404-page-not-found.component';


@NgModule({
  declarations: [
    AppComponent,
    PageNotFoundComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    ModalModule.forRoot(),
    SharedModule,
    HomeModule,
    ScrollToModule.forRoot(),
    AuctionsModule,
    CoreModule.forRoot(),
    // leave routing module as the last one!
    AppRoutingModule
  ],
  providers: [
    PreviousRouteService
  ],
  bootstrap: [
    AppComponent
  ],
  entryComponents: [
    SubscribeEmailComponent,
    SubscribeWhatsappComponent
  ]
})
export class AppModule {
  constructor(private previousRouteService: PreviousRouteService) {
    previousRouteService.init();
  }
}
