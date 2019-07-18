// angular
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

// 3rd lib
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { NgSelectModule } from '@ng-select/ng-select';
import { NgxGalleryModule } from 'ngx-gallery';

// internal
import { SearchComponent } from './components/search/search.component';
import { MinSharedModule } from './shared-min.module';
import { FileUploaderComponent } from './components/file-uploader/file-uploader.component';
import { ImageGalleryComponent } from './components/image-gallery/image-gallery.component';
import { HasRoleDirective } from './directives/authorization/role/has-role.directive';


@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    NgSelectModule,
    NgxDatatableModule,
    MinSharedModule,
    FormsModule,
    ReactiveFormsModule,
    NgxGalleryModule
  ],
  exports: [
    SearchComponent,
    NgSelectModule,
    FormsModule,
    ReactiveFormsModule,
    FileUploaderComponent,
    ImageGalleryComponent,
    HasRoleDirective,
    NgxDatatableModule
  ],
  declarations: [
    SearchComponent,
    FileUploaderComponent,
    ImageGalleryComponent,
    HasRoleDirective
  ]
})
export class SharedModule { }
