// angular
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterModule } from "@angular/router";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

// 3rd lib
import { NgxDatatableModule } from "@swimlane/ngx-datatable";
import { NgSelectModule } from "@ng-select/ng-select";
import { NgxGalleryModule } from "ngx-gallery";
import { TimepickerModule } from "ngx-bootstrap/timepicker";
import { ModalModule } from "ngx-bootstrap/modal";
import { BsDatepickerModule } from "ngx-bootstrap/datepicker";

// internal
import { SearchComponent } from "./components/search/search.component";
import { MinSharedModule } from "./shared-min.module";
import { FileUploaderComponent } from "./components/file-uploader/file-uploader.component";
import { ImageGalleryComponent } from "./components/image-gallery/image-gallery.component";
import { TimepickerComponent } from "./components/timepicker/timepicker.component";

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    NgSelectModule,
    NgxDatatableModule,
    MinSharedModule,
    FormsModule,
    ReactiveFormsModule,
    NgxGalleryModule,
    ModalModule.forRoot(),
    TimepickerModule.forRoot(),
    BsDatepickerModule.forRoot()
  ],
  exports: [
    SearchComponent,
    NgSelectModule,
    FormsModule,
    ReactiveFormsModule,
    FileUploaderComponent,
    ImageGalleryComponent,
    NgxDatatableModule,
    ModalModule,
    MinSharedModule,
    TimepickerModule,
    TimepickerComponent,
    BsDatepickerModule
  ],
  declarations: [
    SearchComponent,
    FileUploaderComponent,
    ImageGalleryComponent,
    TimepickerComponent
  ]
})
export class SharedModule {}
