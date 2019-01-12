import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { RulesRoutingModule } from './rules-routing.module';
import { RulesListComponent } from './components/list/list.component';

@NgModule({
  imports: [
    CommonModule,
    RulesRoutingModule,
    SharedModule
  ],
  declarations: [
    RulesListComponent
  ]
})
export class RulesModule { }
