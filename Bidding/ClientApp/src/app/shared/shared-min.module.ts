// Angular
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

// 3rd lib
import { AccordionModule } from 'ngx-bootstrap/accordion';
import { ContentLoaderModule } from '@netbasal/ngx-content-loader';

// Icons
import {
  FontAwesomeModule,
  FaIconLibrary,
} from '@fortawesome/angular-fontawesome';

import {
  faSearch,
  faExclamationCircle,
  faUsers,
  faPlus,
  faGavel,
} from '@fortawesome/free-solid-svg-icons';

import {
  faHeart,
  faEdit,
  faTrashAlt,
} from '@fortawesome/free-regular-svg-icons';

import {
  faFacebookF,
  faTwitter,
  faWhatsapp,
  faInstagram,
} from '@fortawesome/free-brands-svg-icons';

// Internal
import { NavbarComponent } from './components/navbar/navbar.component';
import { FooterComponent } from './components/footer/footer.component';
import { CallbackComponent } from './components/callback/callback.component';
import { SignInButtonComponent } from './components/sign-in-button/sign-in-button.component';
import { BreadcrumbComponent } from './components/breadcrumb/breadcrumb.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    FontAwesomeModule,
    AccordionModule.forRoot(),
    ContentLoaderModule,
  ],
  exports: [
    NavbarComponent,
    FooterComponent,
    CallbackComponent,
    BreadcrumbComponent,
    FontAwesomeModule,
    ContentLoaderModule,
    AccordionModule,
    SignInButtonComponent,
  ],
  declarations: [
    NavbarComponent,
    FooterComponent,
    CallbackComponent,
    BreadcrumbComponent,
    SignInButtonComponent,
  ],
})
export class MinSharedModule {
  constructor(library: FaIconLibrary) {
    library.addIcons(
      faSearch,
      faExclamationCircle,
      faHeart,
      faFacebookF,
      faTwitter,
      faWhatsapp,
      faInstagram,
      faGavel,
      faUsers,
      faPlus,
      faEdit,
      faTrashAlt
    );
  }
}
