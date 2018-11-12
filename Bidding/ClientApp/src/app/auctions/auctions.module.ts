import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuctionsRoutingModule } from './auctions-routing.module';
import { AuctionListComponent } from './containers/list/list.component';
import { SharedModule } from '../shared/shared.module';
import { AuctionsService } from './services/auctions.service';
import { ReactiveFormsModule } from '@angular/forms';
import { AuctionDetailsComponent } from './containers/details/details.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    AuctionsRoutingModule
  ],
  exports: [
    // AuctionEditComponent,
    // AuctionDeleteComponent,
    // AuctionAddComponent
  ],
  declarations: [
    AuctionListComponent,
    AuctionDetailsComponent
    // AuctionEditComponent,
    // AuctionDeleteComponent,
    // AuctionAddComponent,
    // AuctionCardListComponent
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
