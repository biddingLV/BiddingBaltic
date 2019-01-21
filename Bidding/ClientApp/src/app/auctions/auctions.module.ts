// angular
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms'; // shared?

// routing
import { AuctionsRoutingModule } from './auctions-routing.module';

// modules
import { SharedModule } from '../shared/shared.module';

// services
import { AuctionsService } from './services/auctions.service';

// components
import { AuctionListComponent } from './containers/list/list.component';
import { AuctionDetailsComponent } from './containers/details/details.component';
import { AuctionMainComponent } from './containers/main/main.component';
import { AuctionAddComponent } from './components/add/add.component';
import { AuctionEditComponent } from './components/edit/edit.component';
import { AuctionDeleteComponent } from './components/delete/delete.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    AuctionsRoutingModule
  ],
  exports: [
    AuctionListComponent
  ],
  declarations: [
    AuctionListComponent,
    AuctionDetailsComponent,
    AuctionMainComponent,
    AuctionAddComponent,
    AuctionEditComponent,
    AuctionDeleteComponent
  ],
  providers: [
    AuctionsService
  ],
  entryComponents: [
    AuctionAddComponent,
    AuctionEditComponent,
    AuctionDeleteComponent
  ]
})
export class AuctionsModule { }
