// angular
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// internal
import { SharedModule } from '../shared/shared.module';
import { AuctionsModule } from '../auctions/auctions.module';
import { AdminRoutingModule } from './admin-routing.module';
import { AdminAuctionMainComponent } from './containers/auction-main/auction-main.component';
import { AdminCategoryMainComponent } from './containers/category-main/category-main.component';
import { AdminUserMainComponent } from './containers/user-main/user-main.component';
import { AdminMainComponent } from './containers/main/main.component';


@NgModule({
  imports: [
    CommonModule,
    AdminRoutingModule,
    SharedModule,
    AuctionsModule
  ],
  exports: [

  ],
  declarations: [
    AdminAuctionMainComponent,
    AdminCategoryMainComponent,
    AdminUserMainComponent,
    AdminMainComponent
  ],
  providers: [
  ],
  entryComponents: [

  ]
})
export class AdminModule { }
