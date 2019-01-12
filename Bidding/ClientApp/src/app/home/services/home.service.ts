import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { IEmailSubscribeRequest } from '../models/email-subscribe-request.model';
import { IEmailSubscribeResponse } from '../models/email-subscribe-response.model';
import { catchError } from 'node_modules/rxjs/operators';
import { ExceptionsService } from '../../core';
import { IWhatsAppSubscribeRequest } from '../models/whatsapp-subscribe-request.model';


@Injectable()
export class HomeService {
  constructor(private http: HttpClient, private exception: ExceptionsService) { }

  emailSubscribe$(request: IEmailSubscribeRequest): Observable<boolean> {
    const url = '/subscribe/usingemail';

    return this.http.put<boolean>(url, request)
      .pipe(catchError(this.exception.errorHandler));
  }

  whatsAppSubscribe$(request: IWhatsAppSubscribeRequest): Observable<Object> {
    const url = '/subscribe/usingwhatsapp';

    return this.http.put(url, request)
      .pipe(catchError(this.exception.errorHandler));
  }

  // not used - right now!
  // public submitSurvey$(request: ISurveyRequest): Observable<Object> {
  //   const url = this.baseUrl + '/subscribe/usingsurvey';

  //   return this.http.put(url, request)
  //     .pipe(catchError(this.exception.errorHandler));
  // }
}
