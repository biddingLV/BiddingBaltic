// angular
import { Injectable } from "@angular/core";
import {
  Router,
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  CanLoad,
} from "@angular/router";

// internal
import { AuthService } from "./auth.service";

@Injectable({
  providedIn: "root",
})
export class AuthenticatedGuard implements CanActivate, CanLoad {
  constructor(private authService: AuthService, private router: Router) {}

  canLoad(): boolean {
    if (this.authService.userDetails.IsAuthenticated) {
      return true;
    } else {

      return false;
    }
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    if (this.authService.userDetails.IsAuthenticated) {
      return true;
    } else {

      return false;
    }
  }
}
