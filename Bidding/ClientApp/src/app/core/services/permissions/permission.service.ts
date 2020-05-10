// angular
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

// 3rd lib
import { catchError } from "rxjs/operators";
import { Observable, BehaviorSubject } from "rxjs";

// internal
import { ExceptionsService } from "../exceptions/exceptions.service";
import { NotificationsService } from "../notifications/notifications.service";

@Injectable({
  providedIn: "root"
})
export class PermissionService {
  private _userPermissions: BehaviorSubject<string[]> = new BehaviorSubject(null);
  readonly userPermissions: Observable<string[]> = this._userPermissions.asObservable();

  constructor(
    private httpClient: HttpClient,
    private exceptionService: ExceptionsService,
    private notificationService: NotificationsService
  ) {}

  /**
   * Load and set logged in user's permissions
   */
  setUserPermissions(): void {
    this.loadUserPermissions$().subscribe(
      (response: string[]) => {
        this._userPermissions.next(response);
      },
      (error: string) => this.notificationService.error(error)
    );
  }

  private loadUserPermissions$(): Observable<string[]> {
    const permissionsUrl = "/api/permissions/LoadUserPermissions";

    return this.httpClient.get<string[]>(permissionsUrl).pipe(catchError(this.exceptionService.errorHandler));
  }
}
