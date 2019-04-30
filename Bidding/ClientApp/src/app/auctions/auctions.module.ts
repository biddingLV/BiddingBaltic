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
import { AuctionAddWizardComponent } from './components/add-wizard/add-wizard.component';
import { AuctionAddFirstWizardStepComponent } from './components/add-wizard/wizard-steps/first-step/first-step.component';
import { AuctionAddSecondWizardStepComponent } from './components/add-wizard/wizard-steps/second-step/second-step.component';

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
    AuctionAddWizardComponent,
    AuctionEditComponent,
    AuctionDeleteComponent,
    AuctionAddFirstWizardStepComponent,
    AuctionAddSecondWizardStepComponent
  ],
  providers: [
    AuctionsService
  ],
  entryComponents: [
    AuctionAddWizardComponent,
    AuctionEditComponent,
    AuctionDeleteComponent,
    AuctionAddFirstWizardStepComponent,
    AuctionAddSecondWizardStepComponent
  ]
})
export class AuctionsModule { }
