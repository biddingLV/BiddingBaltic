import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
// import { ProfileComponent } from './profile/profile.component';
// import { PingComponent } from './ping/ping.component';
// import { AdminComponent } from './admin/admin.component';
import { AuthGuardService as AuthGuard } from './auth/auth-guard.service';
import { ScopeGuardService as ScopeGuard } from './auth/scope-guard.service';

const routes: Routes = [
  { path: '', loadChildren: './home/home.module#HomeModule' },
  { path: 'users', loadChildren: './users/users.module#UsersModule' },
  // todo: KKE: canLoad or canActivate?
  { path: 'auctions', loadChildren: './auctions/auctions.module#AuctionsModule' }
  // { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
  // { path: 'ping', component: PingComponent, canActivate: [AuthGuard] },
  // {
  //   path: 'auctions',
  //   canActivate: [AuthGuard],
  //   loadChildren: './auctions/auctions.module#AuctionsModule'
  // },

  // { path: 'admin', component: AdminComponent, canActivate: [ScopeGuard], data: { expectedScopes: ['write:messages'] } },
  // {
  //   path: '',
  //   redirectTo: '',
  //   pathMatch: 'full'
  // },
  // { path: '**', 404}
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
