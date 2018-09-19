import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BlogListComponent } from './containers/list/list.component';
import { BlogDetailsComponent } from './components/details/details.component';

const routes: Routes = [
  { path: 'eksperta-viedoklis', component: BlogListComponent }, // todo: kke: dont use this!
  { path: 'eksperta-viedoklis/:id', component: BlogDetailsComponent }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class BlogRoutingModule { }
