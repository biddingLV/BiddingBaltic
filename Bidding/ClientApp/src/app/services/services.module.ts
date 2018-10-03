import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { ServicesListComponent } from './components/list/list.component';
import { ServicesRoutingModule } from './services-routing.module';

@NgModule({
  imports: [
    CommonModule,
    ServicesRoutingModule,
    SharedModule
  ],
  declarations: [
    ServicesListComponent
  ]
})
export class ServicesModule { }
