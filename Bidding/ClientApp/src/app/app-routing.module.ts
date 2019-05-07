// angular
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// internal
import { GdprRulesComponent } from './shared/components/footer/static-components/gdpr-rules/gdpr-rules.component';
import { AuthGuard } from './core/services/auth/auth-guard.service';
import { PageNotFoundComponent } from './auth/components/404-page-not-found/404-page-not-found.component';
import { PartnerRulesComponent } from './shared/components/footer/static-components/partner-rules/partner-rules.component';
import { RulesListComponent } from './shared/components/footer/static-components/rules-list/rules-list.component';
import { ServiceRulesComponent } from './shared/components/footer/static-components/service-rules/service-rules.component';


const routes: Routes = [
  { path: '', loadChildren: './home/home.module#HomeModule' },
  // { path: 'auctions', loadChildren: './auctions/auctions.module#AuctionsModule' },
  // { path: 'admin', loadChildren: './admin/admin.module#AdminModule' },
  // { path: 'users', loadChildren: './users/users.module#UsersModule' },
  { path: 'public_html', redirectTo: '', pathMatch: 'full' },
  { path: 'noteikumi-un-nosacijumi', component: RulesListComponent },
  { path: 'gdpr', component: GdprRulesComponent },
  { path: 'pakalpojumi', component: ServiceRulesComponent },
  { path: 'sadarbibas-partneru-piedavajumi', component: PartnerRulesComponent },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      scrollPositionRestoration: 'enabled'
    })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
