import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GdprRoutingModule } from './gdpr-routing.module';
import { GdprListComponent } from './components/list/list.component';
import { SharedModule } from 'ClientApp/src/app/shared/shared.module';

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
