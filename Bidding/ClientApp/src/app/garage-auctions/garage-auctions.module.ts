import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GarageAuctionsRoutingModule } from './garage-auctions-routing.module';
import { GarageAuctionMainComponent } from './containers/main/main.component';

@NgModule({
  declarations: [GarageAuctionMainComponent],
  imports: [CommonModule, GarageAuctionsRoutingModule],
})
export class GarageAuctionsModule {}
