import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuctionListComponent } from './containers/list/list.component';
import { AuthGuardService as AuthGuard } from '../auth/auth-guard.service';


const routes: Routes = [
  { path: '', component: AuctionListComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuctionsRoutingModule { }
