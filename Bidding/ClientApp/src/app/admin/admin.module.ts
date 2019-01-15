// angular
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// 3rd lib
import { NgxDatatableModule } from '@swimlane/ngx-datatable'; // todo: kke: do I NEED THIS here?

// routing
import { AdminRoutingModule } from './admin-routing.module';

// internal
import { SharedModule } from '../shared/shared.module';
import { AdminListComponent } from './containers/list/list.component';
import { AuctionsModule } from '../auctions/auctions.module';


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
    AdminListComponent
  ],
  providers: [
  ],
  entryComponents: [

  ]
})
export class AdminModule { }
