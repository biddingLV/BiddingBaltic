// angular
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpRequest } from '@angular/common/http';

// 3rd lib
import { catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';

// internal
import { ExceptionsService } from 'ClientApp/src/app/core';


@Injectable({
  providedIn: 'root'
})
export class FileUploaderService {

  constructor(
    private httpService: HttpClient,
    private exceptionService: ExceptionsService
  ) { }

  uploadFiles$(request: FormData): Observable<boolean> {
    const url = 'api/fileUploader/upload';

    return this.httpService.post<boolean>(url, request)
      .pipe(catchError(this.exceptionService.errorHandler));
  }
}
