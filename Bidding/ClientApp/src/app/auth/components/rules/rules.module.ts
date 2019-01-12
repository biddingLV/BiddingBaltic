// angular
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// internal
import { RulesRoutingModule } from './rules-routing.module';
import { RulesListComponent } from './components/list/list.component';
import { SharedModule } from 'ClientApp/src/app/shared/shared.module';

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
