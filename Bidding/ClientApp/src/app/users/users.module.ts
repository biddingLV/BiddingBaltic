// angular
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// internal
import { UsersRoutingModule } from './users-routing.module';
import { UserDetailsComponent } from './containers/details/details.component';
import { UsersService } from './services/users.service';
import { UsersMainComponent } from './containers/main/main.component';
import { UserEditComponent } from './components/edit/edit.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    UsersRoutingModule,
    SharedModule
  ],
  exports: [
    UserDetailsComponent,
    UsersMainComponent,
    UserEditComponent
  ],
  declarations: [
    UserDetailsComponent,
    UsersMainComponent,
    UserEditComponent
  ],
  providers: [
    UsersService
  ],
  entryComponents: [
    UserEditComponent
  ]
})
export class UsersModule { }
