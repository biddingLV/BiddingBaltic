// angular
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

// third-libraries
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { NgSelectModule } from '@ng-select/ng-select';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

// components
import { AuctionsTableComponent } from './components/table/auctions/auctions.component';
import { DatepickerComponent } from './components/datepicker/datepicker.component';

// filter components
import { SearchComponent } from './components/search/search.component';

// Modules
import { MinSharedModule } from './shared-min.module';
import { FileUploaderComponent } from './components/file-uploader/file-uploader.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    NgSelectModule,
    NgxDatatableModule,
    BsDatepickerModule.forRoot(),
    MinSharedModule,
    FormsModule, // todo: kke: get rid of this module!
    ReactiveFormsModule
  ],
  exports: [
    AuctionsTableComponent,
    SearchComponent,
    NgSelectModule,
    DatepickerComponent,
    FormsModule, // todo: kke: get rid of this module!
    ReactiveFormsModule,
    FileUploaderComponent
  ],
  declarations: [
    AuctionsTableComponent,
    SearchComponent,
    DatepickerComponent,
    FileUploaderComponent
  ]
})
export class SharedModule { }
