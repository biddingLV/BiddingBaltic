import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

// third-libraries
import { NgxDatatableModule } from '@swimlane/ngx-datatable';

// components
import { AuctionsTableComponent } from './components/table/auctions/auctions.component';

// filter components
import { ArrayDropdownComponent } from './components/dropdowns/array/array.component';
import { ObjectDropdownComponent } from './components/dropdowns/object/object.component';
import { SearchComponent } from './components/search/search.component';
import { AdminTableComponent } from './components/table/admin/admin.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    NgxDatatableModule,
    // MomentModule
  ],
  exports: [
    AuctionsTableComponent,
    AdminTableComponent,
    SearchComponent,
    ArrayDropdownComponent,
    ObjectDropdownComponent,
    FormsModule
    // MomentModule
  ],
  declarations: [
    AuctionsTableComponent,
    AdminTableComponent,
    SearchComponent,
    ArrayDropdownComponent,
    ObjectDropdownComponent
  ],
  providers: [

  ]
})
export class SharedModule { }
