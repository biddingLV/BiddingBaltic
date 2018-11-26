import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

// third-libraries
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { MomentModule } from 'ngx-moment';

// components
import { HeaderComponent } from './components/header/header.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FooterComponent } from './components/footer/footer.component';
import { AuctionsTableComponent } from './components/table/auctions/auctions.component';

// filter components
import { ArrayDropdownComponent } from './components/dropdowns/array/array.component';
import { ObjectDropdownComponent } from './components/dropdowns/object/object.component';
import { SearchComponent } from './components/search/search.component';
import { LoadingComponent } from './components/loading/loading.component';
import { AdminTableComponent } from './components/table/admin/admin.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    NgxDatatableModule,
    MomentModule
  ],
  exports: [
    HeaderComponent,
    NavbarComponent,
    FooterComponent,
    AuctionsTableComponent,
    AdminTableComponent,
    SearchComponent,
    ArrayDropdownComponent,
    ObjectDropdownComponent,
    FormsModule,
    LoadingComponent,
    MomentModule
  ],
  declarations: [
    HeaderComponent,
    NavbarComponent,
    FooterComponent,
    AuctionsTableComponent,
    AdminTableComponent,
    SearchComponent,
    ArrayDropdownComponent,
    ObjectDropdownComponent,
    LoadingComponent
  ],
  providers: [

  ]
})
export class SharedModule { }
