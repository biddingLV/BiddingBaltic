// angular
import { NgModule } from "@angular/core";

// internal
import { SharedModule } from "../shared/shared.module";
import { AuctionsModule } from "../auctions/auctions.module";
import { AdminRoutingModule } from "./admin-routing.module";
import { AdminAuctionMainComponent } from "./containers/auction-main/auction-main.component";
import { AdminUserMainComponent } from "./containers/user-main/user-main.component";
import { AdminMainComponent } from "./containers/main/main.component";
import { UserTableComponent } from "./components/user-table/user-table/user-table.component";
import { AdminSidebarComponent } from "./components/sidebar/sidebar.component";
import { UsersModule } from "../users/users.module";

@NgModule({
  imports: [AdminRoutingModule, SharedModule, AuctionsModule, UsersModule],
  exports: [],
  declarations: [
    AdminAuctionMainComponent,
    AdminUserMainComponent,
    AdminMainComponent,
    UserTableComponent,
    AdminSidebarComponent,
  ],
  providers: [],
  entryComponents: [],
})
export class AdminModule {}
