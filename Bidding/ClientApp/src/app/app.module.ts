// angular modules
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
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
import { MinSharedModule } from './shared/shared-min.module';
import { AuthModule } from './auth/auth.module';

// 3rd party modules
import { ScrollToModule } from '@nicky-lenaers/ngx-scroll-to';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AccordionModule } from 'ngx-bootstrap/accordion';

// Services
import { PreviousRouteService } from './shared/services/previous-route.service.ts/previous-route.service';
import { CachingInterceptor } from './core/interceptors/caching/caching-interceptor';
import { RequestCache } from './core/interceptors/caching/request-cache.service';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    ModalModule.forRoot(),
    AccordionModule.forRoot(),
    MinSharedModule,
    SharedModule,
    HomeModule,
    ScrollToModule.forRoot(),
    AuctionsModule,
    CoreModule.forRoot(),
    AuthModule,
    // leave routing module as the last one!
    AppRoutingModule
  ],
  providers: [
    // RequestCache, // todo: kke: test this!
    // { provide: HTTP_INTERCEPTORS, useClass: CachingInterceptor, multi: true }, // todo: kke: test this!
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
