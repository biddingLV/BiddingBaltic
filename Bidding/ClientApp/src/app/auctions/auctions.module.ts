// angular
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// 3rd lib
import { ArchwizardModule } from 'angular-archwizard';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

// internal
import { AuctionDetailsComponent } from './containers/details/details.component';
import { AuctionMainComponent } from './containers/main/main.component';
import { AuctionEditComponent } from './components/edit/edit.component';
import { AuctionDeleteComponent } from './components/delete/delete.component';
import { AuctionsRoutingModule } from './auctions-routing.module';
import { SharedModule } from '../shared/shared.module';
import { AuctionsService } from './services/auctions.service';
import { AuctionAddMainWizardComponent } from './containers/wizard/main/main.component';
import { AuctionAddAddWizardStepComponent } from './components/wizard/wizard-steps/add-step/add-step.component';
import { AuctionAddWizardVehicleComponent } from './components/wizard/wizard-steps/add-step/categories/vehicle/vehicle-category.component';
import { AuctionAddWizardItemComponent } from './components/wizard/wizard-steps/add-step/categories/item/item-category.component';
import { AuctionDetailsCountdownComponent } from './components/details/countdown/countdown.component';
import { AuctionAddCategoryWizardStepComponent } from './components/wizard/wizard-steps/category-step/category-step.component';
import { AuctionAddWizardPropertyComponent } from './components/wizard/wizard-steps/add-step/categories/property/property-category.component';
import { AuctionAddAboutWizardStepComponent } from './components/wizard/wizard-steps/about-step/about-step.component';
import { AuctionTableComponent } from './components/auction-table/auction-table.component';
import { AuctionListComponent } from './containers/list/list.component';


@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    AuctionsRoutingModule,
    ArchwizardModule,
    BsDatepickerModule.forRoot()
  ],
  exports: [
    AuctionTableComponent,
    ArchwizardModule,
    BsDatepickerModule,
    AuctionListComponent
  ],
  declarations: [
    AuctionTableComponent,
    AuctionDetailsComponent,
    AuctionMainComponent,
    AuctionAddMainWizardComponent,
    AuctionEditComponent,
    AuctionDeleteComponent,
    AuctionAddCategoryWizardStepComponent,
    AuctionAddAddWizardStepComponent,
    AuctionAddAboutWizardStepComponent,
    AuctionAddWizardVehicleComponent,
    AuctionAddWizardItemComponent,
    AuctionAddWizardPropertyComponent,
    AuctionDetailsCountdownComponent,
    AuctionListComponent
  ],
  providers: [
    AuctionsService
  ],
  entryComponents: [
    AuctionAddMainWizardComponent,
    AuctionEditComponent,
    AuctionDeleteComponent,
    AuctionAddCategoryWizardStepComponent,
    AuctionAddAddWizardStepComponent,
    AuctionAddAboutWizardStepComponent
  ]
})
export class AuctionsModule { }
