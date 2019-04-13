// angular
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

// 3rd lib
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { NgSelectModule } from '@ng-select/ng-select';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

// filter components
import { GalleryModule } from '@ngx-gallery/core';

// internal
import { AuctionsTableComponent } from './components/table/auctions/auctions.component';
import { SearchComponent } from './components/search/search.component';
import { MinSharedModule } from './shared-min.module';
import { FileUploaderComponent } from './components/file-uploader/file-uploader.component';
import { ImageGalleryComponent } from './components/image-gallery/image-gallery.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    NgSelectModule,
    NgxDatatableModule,
    MinSharedModule,
    FormsModule,
    ReactiveFormsModule,
    GalleryModule,
    BsDatepickerModule.forRoot()
  ],
  exports: [
    AuctionsTableComponent,
    SearchComponent,
    NgSelectModule,
    FormsModule,
    ReactiveFormsModule,
    FileUploaderComponent,
    ImageGalleryComponent,
    BsDatepickerModule
  ],
  declarations: [
    AuctionsTableComponent,
    SearchComponent,
    FileUploaderComponent,
    ImageGalleryComponent
  ]
})
export class SharedModule { }
