// angular
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms'; // shared?

// routing
import { AuctionsRoutingModule } from './auctions-routing.module';

// modules
import { SharedModule } from '../shared/shared.module';

// services
import { AuctionsService } from './services/auctions.service';

// components
import { AuctionListComponent } from './containers/list/list.component';
import { AuctionDetailsComponent } from './containers/details/details.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    AuctionsRoutingModule
  ],
  exports: [
    AuctionListComponent
  ],
  declarations: [
    AuctionListComponent,
    AuctionDetailsComponent
  ],
  providers: [
    AuctionsService
  ],
  entryComponents: [
  ]
})
export class AuctionsModule { }
