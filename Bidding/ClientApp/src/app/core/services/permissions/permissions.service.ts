// angular
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

// 3rd lib
import { shareReplay, map, switchMap } from 'rxjs/operators';
import { Observable, Subject } from 'rxjs';

// internal
import { UserRoleModel } from '../../models/role/user-role.model';


@Injectable({
  providedIn: 'root'
})
export class PermissionsService {
  private userRolePermissions$: Observable<UserRoleModel> = undefined;
  private update$ = new Subject<void>();

  constructor(private http: HttpClient) { }

  getRole(): Observable<UserRoleModel> {
    return this.unpack(data => {
      return {
        roleId: data.roleId,
        roleName: data.roleName,
      };
    });
  }

  getRoleId(): Observable<number> {
    return this.unpack(data => {
      return data.roleId;
    });
  }

  getRoleName(): Observable<string> {
    return this.unpack(data => {
      return data.roleName;
    });
  }

  hasRoleId(roleId: number): Observable<boolean> {
    return this.unpack(data => {
      return data.roleId === roleId;
    });
  }

  hasRoleName(roleName: string): Observable<boolean> {
    return this.unpack(data => {
      return data.roleName == roleName;
    });
  }

  hasAdminRole(): Observable<boolean> {
    return this.unpack(data => {
      return data.roleId === 1;
    });
  }

  hasUserRoleOrGreater(): Observable<boolean> {
    return this.unpack(data => {
      return data.roleId <= 2;
    });
  }

  hasRoles(roles: string[]): Observable<boolean> {
    return this.unpack(data => {
      let hasRole = false;
      for (const role of roles) {
        if (data.roleName === role) {
          hasRole = true;
          break;
        }
      }
      return hasRole;
    });
  }

  update(): void {
    this.update$.next();
  }

  private getUserRolePermissions(): Observable<UserRoleModel> {
    if (!this.userRolePermissions$) {
      this.userRolePermissions$ = this.update$.pipe(
        switchMap(_ => this.getUserRolePermissionsFromServer()),
        shareReplay(1)
      );
      // TODO: Can I avoid doing this? It looks like a hack.
      //       Without it, the first load does not get triggered,
      //       for some reason using an async pipe does not trigger a
      //       reload, but a subscription does?
      this.userRolePermissions$.subscribe(data => { });
      this.update$.next();
    }
    return this.userRolePermissions$;
  }

  private getUserRolePermissionsFromServer(): Observable<UserRoleModel> {
    let url = 'api/Permissions/UserRole';

    return this.http.get<UserRoleModel>(url);
  }

  private unpack<T>(selector: (data: UserRoleModel) => T): Observable<T> {
    return this.getUserRolePermissions().pipe(map(selector));
  }
}
