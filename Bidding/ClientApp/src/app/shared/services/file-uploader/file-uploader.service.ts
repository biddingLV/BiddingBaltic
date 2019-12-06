// angular
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

// 3rd lib
import { catchError } from "rxjs/operators";
import { Observable } from "rxjs";
import { ExceptionsService } from "ClientApp/src/app/core/services/exceptions/exceptions.service";

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

  uploadFiles$(formData: FormData, auctionId: number): Observable<boolean> {
    const url = `api/fileUploader/UploadFiles?auctionId=${auctionId}`;

    return this.httpService
      .post<boolean>(url, formData)
      .pipe(catchError(this.exceptionService.errorHandler));
  }
}
