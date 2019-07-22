// nagular
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// internal
import { AdminAuctionMainComponent } from './containers/auction-main/auction-main.component';
import { AdminUserMainComponent } from './containers/user-main/user-main.component';
import { AdminMainComponent } from './containers/main/main.component';


const routes: Routes = [
  {
    path: '',
    component: AdminMainComponent,
    children: [
      { path: 'auctions', component: AdminAuctionMainComponent },
      { path: 'users', component: AdminUserMainComponent }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
