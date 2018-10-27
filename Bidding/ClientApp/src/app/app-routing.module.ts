import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
// Route guards
import { AuthGuard } from './auth/guards/auth.guard';
import { AdminGuard } from './auth/guards/admin.guard';
// Page components
import { CallbackComponent } from './pages/callback/callback.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { PingComponent } from './pages/ping/ping.component';
import { HomeComponent } from './pages/home/home.component';
// import { ProfileComponent } from './profile/profile.component';
// import { PingComponent } from './ping/ping.component';
// import { AdminComponent } from './admin/admin.component';
// import { AuthGuardService as AuthGuard } from './auth/auth-guard.service';
// import { ScopeGuardService as ScopeGuard } from './auth/scope-guard.service';
// import { BlogDetailsComponent } from './blog/components/details/details.component';
// import { BlogWidgetComponent } from './blog/containers/widget/widget.component';


const routes: Routes = [
  {
    path: '', loadChildren: './home/home.module#HomeModule'
  },
  {
    path: 'noteikumi-un-nosacijumi', loadChildren: './rules/rules.module#RulesModule'
  },
  {
    path: 'gdpr', loadChildren: './gdpr/gdpr.module#GdprModule'
  },
  {
    path: 'pakalpojumi', loadChildren: './services/services.module#ServicesModule'
  },
  {
    path: 'sadarbibas-partneru-piedavajumi', loadChildren: './partners/partners.module#PartnersModule'
  },
  {
    path: 'callback', component: CallbackComponent
  },
  { path: 'profile', component: ProfileComponent },
  { path: 'ping', component: PingComponent },
  { path: 'home', component: HomeComponent },
  {
    path: 'auctions',
    loadChildren: './auctions/auctions.module#AuctionsModule',
    // canActivate: [
    //   AuthGuard
    // ]
  },
  // {
  //   path: 'admin',
  //   loadChildren: './pages/admin/admin.module#AdminModule',
  //   canActivate: [
  //     AuthGuard,
  //     AdminGuard
  //   ]
  // },
  // { path: 'users', loadChildren: './users/users.module#UsersModule' },
  // { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
  // { path: 'ping', component: PingComponent, canActivate: [AuthGuard] },
  // {
  //   path: 'auctions',
  //   canActivate: [AuthGuard],
  //   loadChildren: './auctions/auctions.module#AuctionsModule'
  // },

  // { path: 'admin', component: AdminComponent, canActivate: [ScopeGuard], data: { expectedScopes: ['write:messages'] } },
  {
    path: '**',
    redirectTo: '',
    pathMatch: 'full'
  }
  // { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      scrollPositionRestoration: 'enabled'
    })
  ],
  providers: [
    AuthGuard,
    AdminGuard
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
