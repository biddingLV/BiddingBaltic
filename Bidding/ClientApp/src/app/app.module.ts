// angular
import { NgModule } from "@angular/core";

// internal
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { CoreModule } from "./core/core.module";
import { SharedModule } from "./shared/shared.module";
import { HomeModule } from "./home/home.module";
import { AuctionsModule } from "./auctions/auctions.module";
import { AuthModule } from "./auth/auth.module";
import { TranslocoRootModule } from "./transloco-root.module";

@NgModule({
  declarations: [AppComponent],
  imports: [
    // SharedModule,
    // HomeModule,
    // AuctionsModule,
    CoreModule.forRoot(),
    // AuthModule,
    AppRoutingModule,
    // TranslocoRootModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
