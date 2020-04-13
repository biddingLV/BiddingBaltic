// Angular
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";

// 3rd lib
import { AccordionModule } from "ngx-bootstrap/accordion";
import { ContentLoaderModule } from "@netbasal/ngx-content-loader";

// Icons
import { FontAwesomeModule, FaIconLibrary } from "@fortawesome/angular-fontawesome";

import { faSearch, faExclamationCircle, faUsers, faPlus, faGavel } from "@fortawesome/free-solid-svg-icons";

import { faHeart, faEdit, faTrashAlt } from "@fortawesome/free-regular-svg-icons";

import { faFacebookF, faTwitter, faWhatsapp, faInstagram } from "@fortawesome/free-brands-svg-icons";

// Internal
import { NavbarComponent } from "../core/components/navbar/navbar.component";
import { FooterComponent } from "../core/components/footer/footer.component";
import { CallbackComponent } from "./components/callback/callback.component";
import { GdprRulesComponent } from "../core/components/footer/components/gdpr-rules/gdpr-rules.component";
import { PartnerRulesComponent } from "../core/components/footer/components/partner-rules/partner-rules.component";
import { RulesListComponent } from "../core/components/footer/components/rules-list/rules-list.component";
import { ServiceRulesComponent } from "../core/components/footer/components/service-rules/service-rules.component";
import { FAQPageComponent } from "../core/components/footer/components/faq-page/faq-page.component";
import { SignInButtonComponent } from "./components/sign-in-button/sign-in-button.component";
import { BreadcrumbComponent } from "./components/breadcrumb/breadcrumb.component";

@NgModule({
  imports: [RouterModule, FontAwesomeModule, AccordionModule.forRoot(), ContentLoaderModule],
  exports: [
    NavbarComponent,
    FooterComponent,
    GdprRulesComponent,
    PartnerRulesComponent,
    RulesListComponent,
    ServiceRulesComponent,
    FAQPageComponent,
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
    GdprRulesComponent,
    PartnerRulesComponent,
    RulesListComponent,
    ServiceRulesComponent,
    FAQPageComponent,
    CallbackComponent,
    BreadcrumbComponent,
    SignInButtonComponent,
  ],
})
export class MinSharedModule {
  constructor(library: FaIconLibrary) {
    // Add an icon to the library for convenient access in other components
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
