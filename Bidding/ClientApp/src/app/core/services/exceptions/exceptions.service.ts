// angular
import { Injectable } from "@angular/core";
import { HttpErrorResponse } from "@angular/common/http";

// 3rd lib
import { throwError } from "rxjs";

@Injectable()
export class ExceptionsService {
  constructor() {}

  errorHandler(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred.
      return throwError("Something bad happened; please try again later.");
    } else {
      if (error.status === 500) {
        return throwError("Something bad happened; please try again later.");
      } else if (error.status === 404) {
        // todo: kke: improve error msg, if needed!
        return throwError("It looks like you dont have rights to do this.");
      } else {
        return throwError(error.error);
      }
    }
  }
}
