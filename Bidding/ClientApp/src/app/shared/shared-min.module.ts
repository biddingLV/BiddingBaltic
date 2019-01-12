// Angular
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

// Components
import { NavbarComponent } from './components/navbar/navbar.component';
import { FooterComponent } from './components/footer/footer.component';
import { CallbackComponent } from './components/callback/callback.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [
    NavbarComponent,
    FooterComponent,
    CallbackComponent
  ],
  declarations: [
    NavbarComponent,
    FooterComponent,
    CallbackComponent
  ]
})
export class MinSharedModule { }