// angular
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// internal
import { UsersRoutingModule } from './users-routing.module';
import { UserDetailsComponent } from './containers/details/details.component';
import { UsersService } from './services/users.service';
import { UsersMainComponent } from './containers/main/main.component';


@NgModule({
  imports: [
    CommonModule,
    UsersRoutingModule
  ],
  declarations: [
    UserDetailsComponent,
    UsersMainComponent
  ],
  providers: [
    UsersService
  ],
  entryComponents: [

  ]
})
export class UsersModule { }
