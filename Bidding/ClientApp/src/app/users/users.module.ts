// angular
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

// internal
import { UsersRoutingModule } from "./users-routing.module";
import { UserDetailsComponent } from "./containers/details/details.component";
import { UsersService } from "./services/users.service";
import { UsersMainComponent } from "./containers/main/main.component";
import { UserBasicEditComponent } from "./components/basic-edit/basic-edit.component";
import { SharedModule } from "../shared/shared.module";
import { UserAdvancedEditComponent } from "./components/advanced-edit/advanced-edit.component";

@NgModule({
  imports: [CommonModule, UsersRoutingModule, SharedModule],
  exports: [
    UserDetailsComponent,
    UsersMainComponent,
    UserBasicEditComponent,
    UserAdvancedEditComponent
  ],
  declarations: [
    UserDetailsComponent,
    UsersMainComponent,
    UserBasicEditComponent,
    UserAdvancedEditComponent
  ],
  providers: [UsersService],
  entryComponents: [UserBasicEditComponent, UserAdvancedEditComponent]
})
export class UsersModule {}
