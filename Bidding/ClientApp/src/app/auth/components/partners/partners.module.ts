import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PartnersRoutingModule } from './partners-routing.module';
import { PartnersListComponent } from './components/list/list.component';
import { SharedModule } from 'ClientApp/src/app/shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    PartnersRoutingModule,
    SharedModule
  ],
  declarations: [
    PartnersListComponent
  ]
})
export class PartnersModule { }
