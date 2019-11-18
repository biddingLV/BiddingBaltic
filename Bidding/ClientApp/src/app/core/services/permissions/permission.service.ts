// angular
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

// 3rd lib
import { catchError } from "rxjs/operators";
import { Observable } from "rxjs";

// internal
import { ExceptionsService } from "../exceptions/exceptions.service";

@Injectable({
  providedIn: "root"
})
export class PermissionService {
  constructor(
    private httpClient: HttpClient,
    private exceptionService: ExceptionsService
  ) {}
}
