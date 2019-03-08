// angular
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

// 3rd lib
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

// internal
import { ExceptionsService } from '../../core/services/exceptions/exceptions.service';
import { UserDetailsModel } from '../models/details/user-details.model';


@Injectable({
  providedIn: 'root'
})
export class UsersService {
  constructor(
    private http: HttpClient,
    private exception: ExceptionsService
  ) { }

  getUserDetails$(userId: number): Observable<UserDetailsModel> {
    const url = `/api/users/details?userId=${userId}`;

    return this.http.get<UserDetailsModel>(url)
      .pipe(catchError(this.exception.errorHandler));
  }

  // editUser$(request: IUserEditRequest): Observable<boolean> {
  //   const url = '/api/users/edit';

  //   return this.http.put<boolean>(url, request)
  //     .pipe(catchError(this.exception.errorHandler));
  // }
}
