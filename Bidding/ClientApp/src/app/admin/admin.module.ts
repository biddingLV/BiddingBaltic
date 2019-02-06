// angular
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// 3rd lib
import { NgxDatatableModule } from '@swimlane/ngx-datatable'; // todo: kke: do I NEED THIS here?

// routing
import { AdminRoutingModule } from './admin-routing.module';

// internal
import { SharedModule } from '../shared/shared.module';
import { AuctionsModule } from '../auctions/auctions.module';
import { AdminMainComponent } from './containers/main/main.component';
import { AdminAuctionListComponent } from './containers/auction-list/auction-list.component';
import { AdminCategoryListComponent } from './containers/category-list/category-list.component';


@NgModule({
  imports: [
    CommonModule,
    AdminRoutingModule,
    NgxDatatableModule,
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
