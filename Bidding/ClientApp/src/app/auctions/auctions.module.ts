// angular
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

// 3rd lib
import { ArchwizardModule } from "angular-archwizard";

// internal
import { AuctionDetailsComponent } from "./containers/details/details.component";
import { AuctionMainComponent } from "./containers/main/main.component";
import { AuctionEditComponent } from "./components/edit/edit.component";
import { AuctionDeleteComponent } from "./components/delete/delete.component";
import { AuctionsRoutingModule } from "./auctions-routing.module";
import { SharedModule } from "../shared/shared.module";
import { AuctionsService } from "./services/auctions.service";
import { AuctionAddMainWizardComponent } from "./containers/wizard/main/main.component";
import { AuctionAddAddWizardStepComponent } from "./components/wizard/wizard-steps/add-step/add-step.component";
import { AuctionAddWizardVehicleComponent } from "./components/wizard/wizard-steps/add-step/categories/vehicle/vehicle-category.component";
import { AuctionAddWizardItemComponent } from "./components/wizard/wizard-steps/add-step/categories/item/item-category.component";
import { AuctionDetailsCountdownComponent } from "./components/details/countdown/countdown.component";
import { AuctionAddCategoryWizardStepComponent } from "./components/wizard/wizard-steps/category-step/category-step.component";
import { AuctionAddWizardPropertyComponent } from "./components/wizard/wizard-steps/add-step/categories/property/property-category.component";
import { AuctionAddAboutWizardStepComponent } from "./components/wizard/wizard-steps/about-step/about-step.component";
import { AuctionTableComponent } from "./components/auction-table/auction-table.component";
import { AuctionListComponent } from "./containers/list/list.component";
import { AuctionFiltersComponent } from "./components/auction-filters/auction-filters.component";
import { ItemDetailsComponent } from "./components/details/categories/item-details/item-details.component";
import { PropertyDetailsComponent } from "./components/details/categories/property-details/property-details.component";
import { VehicleDetailsComponent } from "./components/details/categories/vehicle-details/vehicle-details.component";

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    AuctionsRoutingModule,
    ArchwizardModule
  ],
  exports: [
    AuctionTableComponent,
    ArchwizardModule,

    AuctionListComponent,
    AuctionFiltersComponent
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
    AuctionListComponent,
    AuctionFiltersComponent,
    ItemDetailsComponent,
    PropertyDetailsComponent,
    VehicleDetailsComponent
  ],
  providers: [AuctionsService],
  entryComponents: [
    AuctionAddMainWizardComponent,
    AuctionEditComponent,
    AuctionDeleteComponent,
    AuctionAddCategoryWizardStepComponent,
    AuctionAddAddWizardStepComponent,
    AuctionAddAboutWizardStepComponent
  ]
})
export class AuctionsModule {}
