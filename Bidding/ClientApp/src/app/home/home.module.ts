// angular
import { NgModule } from "@angular/core";

// internal
import { HomeRoutingModule } from "./home-routing.module";
import { SharedModule } from "../shared/shared.module";
// import { AuctionsModule } from "../auctions/auctions.module"; AuctionsModule
import { HomeHeaderComponent } from "./components/header/header.component";
import { HomeMainComponent } from "./containers/main/main.component";
import { HomeCardsComponent } from "./components/cards/cards.component";

@NgModule({
  imports: [SharedModule, HomeRoutingModule],
  exports: [],
  declarations: [HomeHeaderComponent, HomeMainComponent, HomeCardsComponent],
  providers: [],
})
export class HomeModule {}
