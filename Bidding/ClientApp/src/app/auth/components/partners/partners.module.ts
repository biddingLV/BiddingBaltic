import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PartnersRoutingModule } from './partners-routing.module';
import { SharedModule } from '../shared/shared.module';
import { PartnersListComponent } from './components/list/list.component';

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
