// Angular
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

// Components
import { NavbarComponent } from './components/navbar/navbar.component';
import { FooterComponent } from './components/footer/footer.component';
import { CallbackComponent } from './components/callback/callback.component';
import { HeaderComponent } from './components/header/header.component';


@NgModule({
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [
    HeaderComponent,
    NavbarComponent,
    FooterComponent,
    CallbackComponent
  ],
  declarations: [
    HeaderComponent,
    NavbarComponent,
    FooterComponent,
    CallbackComponent
  ]
})
export class MinSharedModule { }