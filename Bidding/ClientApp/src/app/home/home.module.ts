// angular
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

// internal
import { HomeRoutingModule } from "./home-routing.module";
import { SharedModule } from "../shared/shared.module";
import { AuctionsModule } from "../auctions/auctions.module";
import { HomeHeaderComponent } from "./components/header/header.component";
import { HomeMainComponent } from "./containers/main/main.component";
import { HomeCardsComponent } from "./components/cards/cards.component";

@NgModule({
  imports: [CommonModule, SharedModule, HomeRoutingModule, AuctionsModule],
  exports: [],
  declarations: [HomeHeaderComponent, HomeMainComponent, HomeCardsComponent],
  providers: [],
})
export class HomeModule {}
