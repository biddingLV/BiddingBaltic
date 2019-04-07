// angular
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// 3rd lib
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

// internal
import { AuctionListComponent } from './containers/list/list.component';
import { AuctionDetailsComponent } from './containers/details/details.component';
import { AuctionMainComponent } from './containers/main/main.component';
import { AuctionAddComponent } from './components/add/add.component';
import { AuctionEditComponent } from './components/edit/edit.component';
import { AuctionDeleteComponent } from './components/delete/delete.component';
import { AuctionsRoutingModule } from './auctions-routing.module';
import { SharedModule } from '../shared/shared.module';
import { AuctionsService } from './services/auctions.service';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    AuctionsRoutingModule,
    BsDatepickerModule.forRoot()
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
