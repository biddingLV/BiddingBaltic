// angular
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// routing
import { AdminRoutingModule } from './admin-routing.module';

// utility
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  imports: [
    CommonModule,
    AdminRoutingModule,
    NgxDatatableModule,
    SharedModule
  ],
  exports: [

  ],
  declarations: [

  ],
  providers: [
  ],
  entryComponents: [

  ]
})
export class AdminModule { }
