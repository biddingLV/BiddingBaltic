import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';

import { HeaderComponent } from './components/header/header.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FooterComponent } from './components/footer/footer.component';
import { AuctionsTableComponent } from './components/table/auctions/auctions.component';

// filter components
import { ArrayDropdownComponent } from './components/dropdowns/array/array.component';
import { ObjectDropdownComponent } from './components/dropdowns/object/object.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    NgxDatatableModule
  ],
  exports: [
    HeaderComponent,
    NavbarComponent,
    FooterComponent,
    AuctionsTableComponent,
    ArrayDropdownComponent,
    ObjectDropdownComponent,
    FormsModule
  ],
  declarations: [
    HeaderComponent,
    NavbarComponent,
    FooterComponent,
    AuctionsTableComponent,
    ArrayDropdownComponent,
    ObjectDropdownComponent
  ],
  providers: [

  ]
})
export class SharedModule { }
