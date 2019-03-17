// Angular
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

// 3rd lib
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
import {
  faSearch
} from '@fortawesome/free-solid-svg-icons';

import {
  faHeart
} from '@fortawesome/free-regular-svg-icons';

// note: kke: for brand icons!
// import {
//   faMicrosoft,
//   faGoogle
// } from '@fortawesome/free-brands-svg-icons';

// Components
import { NavbarComponent } from './components/navbar/navbar.component';
import { FooterComponent } from './components/footer/footer.component';
import { CallbackComponent } from './components/callback/callback.component';
import { HeaderComponent } from './components/header/header.component';


@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    FontAwesomeModule
  ],
  exports: [
    HeaderComponent,
    NavbarComponent,
    FooterComponent,
    CallbackComponent,
    FontAwesomeModule
  ],
  declarations: [
    HeaderComponent,
    NavbarComponent,
    FooterComponent,
    CallbackComponent
  ]
})
export class MinSharedModule {
  constructor() {
    library.add(
      faSearch,
      faHeart
    );
  }
}