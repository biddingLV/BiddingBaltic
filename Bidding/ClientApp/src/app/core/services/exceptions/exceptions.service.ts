import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';

@Injectable()
export class ExceptionsService {
  constructor() { }

  public errorHandler(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred.
      return throwError('Something bad happened; please try again later.');
    } else {
      if (error.status === 500) {
        return throwError(error.status + ' ' + error.statusText + ': ' + 'Something bad happened; please try again later.');
      } else {
        return throwError(error.status + ' ' + error.statusText + ': ' + error.error);
      }
    }
  }
}