import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

// third-libraries
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { NgSelectModule } from '@ng-select/ng-select';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

// components
import { AuctionsTableComponent } from './components/table/auctions/auctions.component';
import { DatepickerComponent } from './components/datepicker/datepicker.component';

// filter components
import { ArrayDropdownComponent } from './components/dropdowns/array/array.component';
import { ObjectDropdownComponent } from './components/dropdowns/object/object.component';
import { SearchComponent } from './components/search/search.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    NgSelectModule,
    FormsModule,
    NgxDatatableModule,
    BsDatepickerModule.forRoot()
  ],
  exports: [
    AuctionsTableComponent,
    SearchComponent,
    ArrayDropdownComponent,
    ObjectDropdownComponent,
    FormsModule,
    NgSelectModule,
    DatepickerComponent
  ],
  declarations: [
    AuctionsTableComponent,
    SearchComponent,
    ArrayDropdownComponent,
    ObjectDropdownComponent,
    DatepickerComponent,
  ],
  providers: [

  ]
})
export class SharedModule { }
