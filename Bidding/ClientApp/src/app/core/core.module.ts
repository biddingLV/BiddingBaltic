// Angular
import { NgModule, Optional, SkipSelf, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// Third Party
import { ToastrModule } from 'ngx-toastr';
import { AngularFontAwesomeModule } from 'angular-font-awesome';

// Services
import { // AuthGuard, PermissionsService, UserService,
  NotificationsService, ExceptionsService
} from './services';
import { HeaderComponent } from './components/header/header.component';
// import { CookieService } from 'ngx-cookie-service';

// Interceptors
// import { JwtInterceptor } from './interceptors/jwt.interceptor'

@NgModule({
  imports: [
    CommonModule,
    BrowserAnimationsModule, // required animations module for toastr
    ToastrModule.forRoot(),
    AngularFontAwesomeModule
  ],
  providers: [
    // { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    // AuthGuard,
    // PermissionsService,
    // UserService,
    NotificationsService,
    ExceptionsService,
    // CookieService,
    HttpClientModule
  ],
  exports: [
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule,
    AngularFontAwesomeModule,
    HeaderComponent
  ],
  declarations: [
    HeaderComponent
  ]
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    if (parentModule) {
      throw new Error(
        'CoreModule is already loaded. Import it in the AppModule only');
    }
  }

  static forRoot(): ModuleWithProviders {
    return {
      ngModule: CoreModule
    };
  }
}
