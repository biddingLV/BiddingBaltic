// Angular
import {
  NgModule,
  Optional,
  SkipSelf,
  ModuleWithProviders
} from "@angular/core";
import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";

// Third Party
import { ToastrModule } from "ngx-toastr";
import { CookieService } from "ngx-cookie-service";

// Internal
import { CategoryConstants, RoleConstants } from "./constants";
import { AuthenticatedGuard } from "./services/auth/auth-guard.service";
import { PermissionService } from "./services/permissions/permission.service";
import { AuthService } from "./services/auth/auth.service";
import { NotificationsService } from "./services/notifications/notifications.service";
import { ExceptionsService } from "./services/exceptions/exceptions.service";
import { FormService } from "./services/form/form.service";
import { ModalService } from "./services/modal/modal.service";

@NgModule({
  imports: [
    CommonModule,
    BrowserAnimationsModule, // required animations module for toastr
    ToastrModule.forRoot()
  ],
  providers: [
    AuthenticatedGuard,
    PermissionService,
    AuthService,
    NotificationsService,
    ExceptionsService,
    FormService,
    CookieService,
    HttpClientModule,
    CategoryConstants, // todo: kke: is this correct?
    RoleConstants, // todo: kke: is this correct?
    ModalService
  ],
  exports: [HttpClientModule, BrowserAnimationsModule, ToastrModule],
  declarations: []
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    if (parentModule) {
      throw new Error(
        "CoreModule is already loaded. Import it in the AppModule only"
      );
    }
  }

  static forRoot(): ModuleWithProviders {
    return {
      ngModule: CoreModule
    };
  }
}
