// angular
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// internal
import { GdprRulesComponent } from './static/components/gdpr-rules/gdpr-rules.component';
import { PageNotFoundComponent } from './auth/components/404-page-not-found/404-page-not-found.component';
import { PartnerRulesComponent } from './static/components/partner-rules/partner-rules.component';
import { RulesListComponent } from './static/components/rules-list/rules-list.component';
import { ServiceRulesComponent } from './static/components/service-rules/service-rules.component';
import { FAQPageComponent } from './static/components/faq-page/faq-page.component';
import { AuthenticatedGuard } from './core/services/auth/auth-guard.service';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./home/home.module').then((m) => m.HomeModule),
  },
  {
    path: 'izsoles',
    loadChildren: () =>
      import('./auctions/auctions.module').then((m) => m.AuctionsModule),
  },
  {
    path: 'admin',
    canLoad: [AuthenticatedGuard],
    loadChildren: () =>
      import('./admin/admin.module').then((m) => m.AdminModule),
  },
  {
    path: 'users',
    loadChildren: () =>
      import('./users/users.module').then((m) => m.UsersModule),
  },
  { path: 'public_html', redirectTo: '', pathMatch: 'full' },
  { path: 'noteikumi-un-nosacijumi', component: RulesListComponent },
  { path: 'gdpr', component: GdprRulesComponent },
  { path: 'pakalpojumi', component: ServiceRulesComponent },
  { path: 'sadarbibas-partneru-piedavajumi', component: PartnerRulesComponent },
  { path: 'faq', component: FAQPageComponent },
  {
    path: 'garazu-izsoles',
    loadChildren: () =>
      import('./garage-auctions/garage-auctions.module').then(
        (m) => m.GarageAuctionsModule
      ),
  },
  { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      scrollPositionRestoration: 'enabled',
    }),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
