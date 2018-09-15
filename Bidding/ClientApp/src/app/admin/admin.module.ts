import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { SharedModule } from '../shared/shared.module';
import { AuctionDeleteComponent } from './components/delete/delete.component';
import { AuctionAddComponent } from './components/add/add.component';
import { AuctionEditComponent } from './components/edit/edit.component';



@NgModule({
  imports: [
    CommonModule,
    AdminRoutingModule,
    NgxDatatableModule,
    SharedModule
  ],
  exports: [
    AuctionEditComponent,
    AuctionDeleteComponent,
    AuctionAddComponent
  ],
  declarations: [
    AuctionEditComponent,
    AuctionDeleteComponent,
    AuctionAddComponent
  ],
  providers: [
  ],
  entryComponents: [

  ]
})
export class AdminsModule { }
