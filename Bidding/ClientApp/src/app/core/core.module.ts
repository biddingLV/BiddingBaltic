// Angular
import { NgModule, Optional, SkipSelf, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';

// Third Party
import { ToastrModule } from 'ngx-toastr';
import { AngularFontAwesomeModule } from 'angular-font-awesome';

// Services
import { // AuthGuard, PermissionsService, UserService,
  NotificationsService, ExceptionsService, FormService, CustomValidators
} from './services';
import { HeaderComponent } from './components/header/header.component';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
// import { CookieService } from 'ngx-cookie-service';

// Interceptors
// import { JwtInterceptor } from './interceptors/jwt.interceptor'

@NgModule({
  imports: [
    CommonModule,
    ToastrModule.forRoot(),
    AngularFontAwesomeModule,
    HttpClientModule,
    RouterModule,
    ReactiveFormsModule
  ],
  providers: [
    // { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    // AuthGuard,
    // PermissionsService,
    // UserService,
    NotificationsService,
    ExceptionsService,
    FormService,
    // CookieService,
  ],
  exports: [
    HttpClientModule,
    RouterModule,
    ReactiveFormsModule,
    ToastrModule,
    AngularFontAwesomeModule,
    HeaderComponent,
    // todo: kke: implement this!
    // FooterComponent
  ],
  declarations: [
    // todo: kke: implement this!
    HeaderComponent,
    // FooterComponent
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
