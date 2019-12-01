// angular
import { Injectable } from "@angular/core";
import { HttpClient, HttpParams, HttpRequest } from "@angular/common/http";

// 3rd lib
import { catchError } from "rxjs/operators";
import { Observable } from "rxjs";
import { ExceptionsService } from "ClientApp/src/app/core/services/exceptions/exceptions.service";

// internal

@Injectable({
  providedIn: "root"
})
export class FileUploaderService {
  constructor(
    private httpService: HttpClient,
    private exceptionService: ExceptionsService
  ) {}

  validateFiles$(request: FormData): Observable<boolean> {
    const url = "api/fileUploader/ValidateFiles";

    return this.httpService
      .post<boolean>(url, request)
      .pipe(catchError(this.exceptionService.errorHandler));
  }

  uploadFiles$(request: FormData): Observable<boolean> {
    const url = "api/fileUploader/UploadFiles";

    return this.httpService
      .post<boolean>(url, request)
      .pipe(catchError(this.exceptionService.errorHandler));
  }
}
