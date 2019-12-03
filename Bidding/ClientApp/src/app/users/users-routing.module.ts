// angular
import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

// internal
import { UserDetailsComponent } from "./containers/details/details.component";
import { UsersMainComponent } from "./containers/main/main.component";

const routes: Routes = [
  { path: "", component: UsersMainComponent },
  { path: "details/:id", component: UserDetailsComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule {}
