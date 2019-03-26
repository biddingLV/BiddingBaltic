// angular
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// internal
import { SharedModule } from '../shared/shared.module';
import { AuctionsModule } from '../auctions/auctions.module';
import { AdminMainComponent } from './containers/main/main.component';
import { AdminAuctionListComponent } from './containers/auction-list/auction-list.component';
import { AdminCategoryListComponent } from './containers/category-list/category-list.component';
import { AdminRoutingModule } from './admin-routing.module';


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
    AdminMainComponent,
    AdminAuctionListComponent,
    AdminCategoryListComponent
  ],
  providers: [
  ],
  entryComponents: [

  ]
})
export class AdminModule { }
