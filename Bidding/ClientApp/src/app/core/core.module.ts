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

// Services
import {
  AuthenticatedGuard,
  PermissionService,
  AuthService,
  NotificationsService,
  ExceptionsService,
  FormService,
  ModalService
} from "./services";

// Constants
import { CategoryConstants, RoleConstants } from "./constants";

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
