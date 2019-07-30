// angular
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// 3rd lib
import { ScrollToModule } from '@nicky-lenaers/ngx-scroll-to'; // todo: kke: Do we need this still?
import { ModalModule } from 'ngx-bootstrap/modal'; // todo: kke: can we move this somewhere?
import { AccordionModule } from 'ngx-bootstrap/accordion'; // todo: kke: can we move this somewhere?

// internal
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { HomeModule } from './home/home.module';
import { AuctionsModule } from './auctions/auctions.module';
import { AuthModule } from './auth/auth.module';
import { PreviousRouteService } from './shared/services/previous-route.service.ts/previous-route.service';


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
    PreviousRouteService
  ],
  bootstrap: [
    AppComponent
  ],
  entryComponents: []
})
export class AppModule {
  constructor(private previousRouteService: PreviousRouteService) {
    previousRouteService.init(); // todo: kke: what is this nightmare?
  }
}
