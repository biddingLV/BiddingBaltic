// angular
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// internal - services
import { NoAuthGuard } from '../core/services/auth/no-auth-guard.service';

const routes: Routes = [

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule { }
