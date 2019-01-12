import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GdprRoutingModule } from './gdpr-routing.module';
import { SharedModule } from '../shared/shared.module';
import { GdprListComponent } from './components/list/list.component';

@NgModule({
  imports: [
    CommonModule,
    GdprRoutingModule,
    SharedModule
  ],
  declarations: [
    GdprListComponent
  ]
})
export class GdprModule { }
