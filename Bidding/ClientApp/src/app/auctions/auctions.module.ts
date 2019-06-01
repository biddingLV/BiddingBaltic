// angular
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// 3rd lib
import { ArchwizardModule } from 'angular-archwizard';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

// internal 
import { AuctionListComponent } from './containers/list/list.component';
import { AuctionDetailsComponent } from './containers/details/details.component';
import { AuctionMainComponent } from './containers/main/main.component';
import { AuctionEditComponent } from './components/edit/edit.component';
import { AuctionDeleteComponent } from './components/delete/delete.component';
import { AuctionsRoutingModule } from './auctions-routing.module';
import { SharedModule } from '../shared/shared.module';
import { AuctionsService } from './services/auctions.service';
import { AuctionAddMainWizardComponent } from './containers/wizard/main/main.component';
import { AuctionAddFirstWizardStepComponent } from './components/wizard/wizard-steps/first-step/first-step.component';
import { AuctionAddLastWizardStepComponent } from './components/wizard/wizard-steps/last-step/last-step.component';
import { AuctionAddAddWizardStepComponent } from './components/wizard/wizard-steps/add-step/add-step.component';
import { AuctionAddWizardVehicleComponent } from './components/wizard/wizard-steps/add-step/categories/vehicle/vehicle-category.component';
import { AuctionAddWizardItemComponent } from './components/wizard/wizard-steps/add-step/categories/item/item-category.component';
import { AuctionAddWizardEstateComponent } from './components/wizard/wizard-steps/add-step/categories/estate/estate-category.component';
import { AuctionDetailsCountdownComponent } from './components/details/countdown/countdown.component';



@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    AuctionsRoutingModule,
    ArchwizardModule,
    BsDatepickerModule.forRoot()
  ],
  exports: [
    AuctionListComponent,
    ArchwizardModule,
    BsDatepickerModule
  ],
  declarations: [
    AuctionListComponent,
    AuctionDetailsComponent,
    AuctionMainComponent,
    AuctionAddMainWizardComponent,
    AuctionEditComponent,
    AuctionDeleteComponent,
    AuctionAddFirstWizardStepComponent,
    AuctionAddAddWizardStepComponent,
    AuctionAddLastWizardStepComponent,
    AuctionAddWizardVehicleComponent,
    AuctionAddWizardItemComponent,
    AuctionAddWizardEstateComponent,
    AuctionDetailsCountdownComponent
  ],
  providers: [
    AuctionsService
  ],
  entryComponents: [
    AuctionAddMainWizardComponent,
    AuctionEditComponent,
    AuctionDeleteComponent,
    AuctionAddFirstWizardStepComponent,
    AuctionAddAddWizardStepComponent,
    AuctionAddLastWizardStepComponent
  ]
})
export class AuctionsModule { }
