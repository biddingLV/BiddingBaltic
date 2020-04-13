// angular
import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

// internal
// import { GdprRulesComponent } from "./core/components/footer/static-components/gdpr-rules/gdpr-rules.component";
// import { PageNotFoundComponent } from "./auth/components/404-page-not-found/404-page-not-found.component";
// import { PartnerRulesComponent } from "./core/components/footer/static-components/partner-rules/partner-rules.component";
// import { RulesListComponent } from "./core/components/footer/static-components/rules-list/rules-list.component";
// import { ServiceRulesComponent } from "./core/components/footer/static-components/service-rules/service-rules.component";
// import { FAQPageComponent } from "./core/components/footer/static-components/faq-page/faq-page.component";
import { AuthenticatedGuard } from "./core/services/auth/auth-guard.service";

const routes: Routes = [
  {
    path: "",
    pathMatch: "full",
    redirectTo: "home",
  },
  {
    path: "home",
    loadChildren: () => import("./home/home.module").then((m) => m.HomeModule),
  },
  {
    path: "izsoles",
    loadChildren: () => import("./auctions/auctions.module").then((m) => m.AuctionsModule),
  },
  {
    path: "admin",
    canLoad: [AuthenticatedGuard],
    loadChildren: () => import("./admin/admin.module").then((m) => m.AdminModule),
  },
  {
    path: "users",
    loadChildren: () => import("./users/users.module").then((m) => m.UsersModule),
  },
  { path: "public_html", redirectTo: "", pathMatch: "full" },
  // { path: "noteikumi-un-nosacijumi", component: RulesListComponent },
  // { path: "gdpr", component: GdprRulesComponent },
  // { path: "pakalpojumi", component: ServiceRulesComponent },
  // { path: "sadarbibas-partneru-piedavajumi", component: PartnerRulesComponent },
  // { path: "faq", component: FAQPageComponent },
  // { path: "**", component: PageNotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
