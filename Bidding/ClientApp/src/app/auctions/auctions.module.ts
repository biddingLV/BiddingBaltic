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
import { AuctionEditComponent } from '../admin/components/edit/edit.component';
import { AuctionDeleteComponent } from '../admin/components/delete/delete.component';
import { AuctionAddComponent } from '../admin/components/add/add.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    AuctionsRoutingModule
  ],
  exports: [
    AuctionEditComponent,
    AuctionDeleteComponent,
    AuctionAddComponent
  ],
  declarations: [
    AuctionListComponent,
    AuctionDetailsComponent,
    AuctionEditComponent,
    AuctionDeleteComponent,
    AuctionAddComponent,
  ],
  providers: [
    AuctionsService
  ],
  entryComponents: [
    AuctionEditComponent,
    AuctionDeleteComponent,
    AuctionAddComponent
  ]
})
export class AuctionsModule { }
