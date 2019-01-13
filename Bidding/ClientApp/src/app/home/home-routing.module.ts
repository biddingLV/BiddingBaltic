import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ComingSoonListComponent } from './containers/coming-soon-list/list.component';

const routes: Routes = [
  { path: '', component: ComingSoonListComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
