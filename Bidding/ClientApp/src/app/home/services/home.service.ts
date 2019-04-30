import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

// 3rd lib
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

// internal
import { ExceptionsService } from '../../core/services/exceptions/exceptions.service';
import { IEmailSubscribeRequest } from '../models/email-subscribe-request.model';
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
}
