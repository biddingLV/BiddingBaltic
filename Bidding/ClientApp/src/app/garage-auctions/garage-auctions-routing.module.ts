import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { GarageAuctionMainComponent } from './containers/main/main.component';

const routes: Routes = [{ path: '', component: GarageAuctionMainComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class GarageAuctionsRoutingModule {}
