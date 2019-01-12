import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GdprListComponent } from './components/list/list.component';

const routes: Routes = [
  { path: '', component: GdprListComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GdprRoutingModule { }
