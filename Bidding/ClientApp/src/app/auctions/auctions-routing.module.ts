// angular
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// internal
import { AuctionDetailsComponent } from './containers/details/details.component';
import { AuctionMainComponent } from './containers/main/main.component';

const routes: Routes = [
  { path: '', component: AuctionMainComponent },
  { path: 'auction/:id', component: AuctionDetailsComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuctionsRoutingModule { }
