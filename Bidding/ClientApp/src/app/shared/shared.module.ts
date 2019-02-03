import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

// third-libraries
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { NgSelectModule } from '@ng-select/ng-select';

// components
import { AuctionsTableComponent } from './components/table/auctions/auctions.component';

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
    NgxDatatableModule
  ],
  exports: [
    AuctionsTableComponent,
    SearchComponent,
    ArrayDropdownComponent,
    ObjectDropdownComponent,
    FormsModule,
    NgSelectModule
  ],
  declarations: [
    AuctionsTableComponent,
    SearchComponent,
    ArrayDropdownComponent,
    ObjectDropdownComponent
  ],
  providers: [

  ]
})
export class SharedModule { }
