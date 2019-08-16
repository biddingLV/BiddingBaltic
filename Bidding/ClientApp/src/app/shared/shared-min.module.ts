// Angular
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterModule } from "@angular/router";

// 3rd lib
import { FontAwesomeModule } from "@fortawesome/angular-fontawesome";
import { AccordionModule } from "ngx-bootstrap/accordion";
import { ContentLoaderModule } from "@netbasal/ngx-content-loader";
import { library } from "@fortawesome/fontawesome-svg-core";
import {
  faSearch,
  faExclamationCircle
} from "@fortawesome/free-solid-svg-icons";

import { faHeart } from "@fortawesome/free-regular-svg-icons";

// note: kke: for brand icons!
// import {
//   faMicrosoft,
//   faGoogle
// } from '@fortawesome/free-brands-svg-icons';

// Components
import { NavbarComponent } from "./components/navbar/navbar.component";
import { FooterComponent } from "./components/footer/footer.component";
import { CallbackComponent } from "./components/callback/callback.component";
import { GdprRulesComponent } from "./components/footer/static-components/gdpr-rules/gdpr-rules.component";
import { PartnerRulesComponent } from "./components/footer/static-components/partner-rules/partner-rules.component";
import { RulesListComponent } from "./components/footer/static-components/rules-list/rules-list.component";
import { ServiceRulesComponent } from "./components/footer/static-components/service-rules/service-rules.component";
import { FAQPageComponent } from "./components/footer/static-components/faq-page/faq-page.component";
import { HomeSignUpButtonComponent } from "./components/home-sign-up-button/home-sign-up-button.component";

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    FontAwesomeModule,
    AccordionModule,
    ContentLoaderModule
  ],
  exports: [
    NavbarComponent,
    FooterComponent,
    GdprRulesComponent,
    PartnerRulesComponent,
    RulesListComponent,
    ServiceRulesComponent,
    FAQPageComponent,
    CallbackComponent,
    FontAwesomeModule,
    ContentLoaderModule,
    HomeSignUpButtonComponent
  ],
  declarations: [
    NavbarComponent,
    FooterComponent,
    GdprRulesComponent,
    PartnerRulesComponent,
    RulesListComponent,
    ServiceRulesComponent,
    FAQPageComponent,
    CallbackComponent,
    HomeSignUpButtonComponent
  ]
})
export class MinSharedModule {
  constructor() {
    library.add(faSearch, faHeart, faExclamationCircle);
  }
}
