// nagular
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// internal
import { AdminMainComponent, } from './containers/main/main.component';
import { AdminCategoryListComponent } from './containers/category-list/category-list.component';

const routes: Routes = [
  { path: '', component: AdminMainComponent },
  { path: '/kategorijas', component: AdminCategoryListComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
