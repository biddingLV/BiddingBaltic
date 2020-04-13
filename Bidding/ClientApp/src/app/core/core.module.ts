// Angular
import { NgModule, Optional, SkipSelf, ModuleWithProviders } from "@angular/core";
import { HttpClientModule } from "@angular/common/http";
import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { RouterModule } from "@angular/router";

// Third Party
import { ToastrModule } from "ngx-toastr";
import { CookieService } from "ngx-cookie-service";

// Internal
import { AuthenticatedGuard } from "./services/auth/auth-guard.service";
import { PermissionService } from "./services/permissions/permission.service";
import { AuthService } from "./services/auth/auth.service";
import { NotificationsService } from "./services/notifications/notifications.service";
import { ExceptionsService } from "./services/exceptions/exceptions.service";
import { FormService } from "./services/form/form.service";
import { ModalService } from "./services/modal/modal.service";
import { ButtonsService } from "./services/buttons/buttons.service";
import { MainLayoutComponent } from "./layout/main-layout/main-layout.component";

@NgModule({
  imports: [ToastrModule.forRoot(), BrowserModule, BrowserAnimationsModule, RouterModule],
  providers: [
    AuthenticatedGuard,
    PermissionService,
    AuthService,
    NotificationsService,
    ExceptionsService,
    FormService,
    CookieService,
    HttpClientModule,
    ModalService,
    ButtonsService,
  ],
  exports: [HttpClientModule, ToastrModule, MainLayoutComponent],
  declarations: [MainLayoutComponent],
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    if (parentModule) {
      throw new Error("CoreModule is already loaded. Import it in the AppModule only");
    }
  }

  static forRoot(): ModuleWithProviders<CoreModule> {
    return {
      ngModule: CoreModule,
    };
  }
}
