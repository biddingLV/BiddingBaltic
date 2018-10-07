import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuctionsRoutingModule } from './auctions-routing.module';
import { AuctionListComponent } from './containers/list/list.component';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { SharedModule } from '../shared/shared.module';
import { AuctionsService } from './services/auctions.service';
import { AuctionEditComponent } from './components/edit/edit.component';
import { AuctionDeleteComponent } from './components/delete/delete.component';
import { AuctionAddComponent } from './components/add/add.component';
import { AuctionCardListComponent } from './containers/card/list/list.component';
import { LoadingComponent } from '../core/components/loading/loading.component';
import { CoreModule } from '../core';


@NgModule({
  imports: [
    CommonModule,
    NgxDatatableModule,
    SharedModule,
    AuctionsRoutingModule,
    CoreModule
  ],
  exports: [
    AuctionEditComponent,
    AuctionDeleteComponent,
    AuctionAddComponent
  ],
  declarations: [
    AuctionListComponent,
    AuctionEditComponent,
    AuctionDeleteComponent,
    AuctionAddComponent,
    AuctionCardListComponent
  ],
  providers: [
    AuctionsService
  ],
  entryComponents: [
    // AuctionEditComponent,
    // AuctionDeleteComponent,
    // AuctionAddComponent
  ]
})
export class AuctionsModule { }
