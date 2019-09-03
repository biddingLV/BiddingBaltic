// angular
import { Injectable } from "@angular/core";
import {
  Router,
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  CanLoad
} from "@angular/router";

// internal
import { AuthService } from "./auth.service";

@Injectable({
  providedIn: "root"
})
export class AuthenticatedGuard implements CanActivate, CanLoad {
  constructor(private authService: AuthService, private router: Router) {}

  canLoad(): boolean {
    console.log("called! canLoad");
    if (this.authService.userDetails.IsAuthenticated) {
      return true;
    } else {
      // todo: kke: what about this part?
      // if (next.routeConfig && next.routeConfig.path !== "**") {
      //   this.authService.redirectUri = state.url;
      // }

      return false;
    }
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    console.log("called! canActivate");
    if (this.authService.userDetails.IsAuthenticated) {
      return true;
    } else {
      // todo: kke: what about this part?
      // if (next.routeConfig && next.routeConfig.path !== "**") {
      //   this.authService.redirectUri = state.url;
      // }

      return false;
    }
  }
}
