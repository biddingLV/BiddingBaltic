import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UsersDetailComponent } from './containers/details/details.component';
// import { AuthGuardService as AuthGuard } from '../auth/auth-guard.service';

const routes: Routes = [
  // { path: 'users/details/:id', canLoad: [AuthGuard], component: UsersDetailComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
