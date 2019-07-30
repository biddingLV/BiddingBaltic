// angular
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// internal
import { HomeMainComponent } from './containers/main/main.component';


const routes: Routes = [
  { path: '', component: HomeMainComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
