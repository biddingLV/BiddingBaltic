import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, timer, Subject } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class PermissionsService {
  private update$ = new Subject<void>();
  private url = 'api/Permissions/User';

  constructor(private http: HttpClient) { }

  // public getRole(): Observable<IRoleFilter> {
  //   return this.unpack(data => {
  //     return {
  //       RoleId: data.RoleId,
  //       RoleName: data.RoleName,
  //     };
  //   });
  // }

  // public getRoleId(): Observable<number> {
  //   return this.unpack(data => {
  //     return data.RoleId;
  //   });
  // }

  // public getRoleName(): Observable<string> {
  //   return this.unpack(data => {
  //     return data.RoleName;
  //   });
  // }

  // public hasRoleId(roleId: number): Observable<boolean> {
  //   return this.unpack(data => {
  //     return data.RoleId === roleId;
  //   });
  // }

  // public hasRoleName(roleName: string): Observable<boolean> {
  //   return this.unpack(data => {
  //     return data.RoleName === roleName;
  //   });
  // }

  // public hasAdminRole(): Observable<boolean> {
  //   return this.unpack(data => {
  //     return data.RoleId === 1;
  //   });
  // }

  // public hasUserRoleOrGreater(): Observable<boolean> {
  //   return this.unpack(data => {
  //     return data.RoleId <= 2;
  //   });
  // }

  // public hasRoles(roles: string[]): Observable<boolean> {
  //   return this.unpack(data => {
  //     let hasRole = false;
  //     for (const role of roles) {
  //       if (data.RoleName === role) {
  //         hasRole = true;
  //         break;
  //       }
  //     }
  //     return hasRole;
  //   });
  // }

  // public getPermissions(): Observable<IAssignedPermission[]> {
  //   return this.unpack(data => {
  //     return data.Permissions;
  //   });
  // }

  // public hasPermissionAccessById(permissionId: number): Observable<boolean> {
  //   return this.unpack(data => {
  //     return (data.Permissions
  //       .find(p => p.PermissionId === permissionId && p.Access === true) !== undefined);
  //   });
  // }

  // public hasPermissionAccessByName(permissionName: string): Observable<boolean> {
  //   return this.unpack(data => {
  //     return (data.Permissions
  //       .find(p => p.PermissionName === permissionName && p.Access === true) !== undefined);
  //   });
  // }

  // public hasPermissionsAccess(permissions: string[]): Observable<boolean> {
  //   return this.unpack(data => {
  //     let found = true;
  //     for (const permission of permissions) {
  //       if (data.Permissions.findIndex(p => p.PermissionName === permission && p.Access) === -1) {
  //         found = false;
  //         break;
  //       }
  //     }
  //     return found;
  //   });
  // }

  // public hasPermissionGrantById(permissionId: number): Observable<boolean> {
  //   return this.unpack(data => {
  //     return (data.Permissions
  //       .find(p => p.PermissionId === permissionId && p.PermissionGrant === true) !== undefined);
  //   });
  // }

  // public hasPermissionGrantByName(permissionName: string): Observable<boolean> {
  //   return this.unpack(data => {
  //     return (data.Permissions
  //       .find(p => p.PermissionName === permissionName && p.PermissionGrant === true) !== undefined);
  //   });

  // }

  // public hasPermissionsGrant(permissions: string[]): Observable<boolean> {
  //   return this.unpack(data => {
  //     let found = true;
  //     for (const permission of permissions) {
  //       if (data.Permissions.findIndex(p => p.PermissionName === permission && p.PermissionGrant) === -1) {
  //         found = false;
  //         break;
  //       }
  //     }
  //     return found;
  //   });
  // }

  // private getUserRolePermissions(): Observable<IUserRolePermissions> {
  //   if (!this.userRolePermissions$) {
  //     this.userRolePermissions$ = this.update$.pipe(
  //       switchMap(_ => this.getUserRolePermissionsFromServer()),
  //       shareReplay(1)
  //     );
  //     // TODO: MJU: Can I avoid doing this? It looks like a hack.
  //     //            Without it, the first load does not get triggered,
  //     //            for some reason using an async pipe does not trigger a
  //     //            reload, but a subscription does?
  //     this.userRolePermissions$.subscribe(data => { });
  //     this.update$.next();
  //   }
  //   return this.userRolePermissions$;
  // }

  // public update(): void {
  //   this.update$.next();
  // }

  // private getUserRolePermissionsFromServer(): Observable<IUserRolePermissions> {
  //   return this.http.get<IUserRolePermissions>(this.url);
  // }

  // private unpack<T>(selector: (data: IUserRolePermissions) => T): Observable<T> {
  //   return this.getUserRolePermissions().pipe(map(selector));
  // }

  /*    const timer$ = timer(0, 10000); // TODO: MJU: Use something else, only example to use timer.
   this.userRolePermissions$ = timer$.pipe(
     switchMap(_ => this.getUserRolePermissionsFromServer()),
     shareReplay(1)
   );  */
}
