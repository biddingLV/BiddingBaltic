// angular
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// 3rd lib
import { ArchwizardModule } from 'angular-archwizard';
import { CountdownModule } from 'ngx-countdown';

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


@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    AuctionsRoutingModule,
    ArchwizardModule,
    CountdownModule
  ],
  exports: [
    AuctionListComponent,
    ArchwizardModule
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
    AuctionAddLastWizardStepComponent
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
