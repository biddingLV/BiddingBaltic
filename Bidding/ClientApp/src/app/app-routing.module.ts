// angular
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// internal
import { AuthGuard } from './core/services/auth/auth-guard.service';
import { PageNotFoundComponent } from './auth/components/404-page-not-found/404-page-not-found.component';

const routes: Routes = [
  { path: '', loadChildren: './home/home.module#HomeModule' },
  { path: 'home', loadChildren: './home/home.module#HomeModule' },
  { path: 'auctions', loadChildren: './auctions/auctions.module#AuctionsModule' },
  { path: 'admin', loadChildren: './admin/admin.module#AdminModule' },
  { path: 'users', loadChildren: './users/users.module#UsersModule' },
  { path: 'public_html', redirectTo: '', pathMatch: 'full' },
  { path: '**', canActivate: [AuthGuard], component: PageNotFoundComponent, data: { breadcrumb: 'Not Found', hideBreadcrumb: true, title: 'Not Found' } }
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
