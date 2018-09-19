import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BlogRoutingModule } from './blog-routing.module';
import { BlogListComponent } from './containers/list/list.component';
import { BlogDetailsComponent } from './components/details/details.component';
import { BlogService } from './services/blog.service';


@NgModule({
  imports: [
    CommonModule,
    BlogRoutingModule
  ],
  declarations: [
    BlogListComponent,
    BlogDetailsComponent
  ],
  providers: [
    BlogService
  ]
})
export class BlogModule { }
