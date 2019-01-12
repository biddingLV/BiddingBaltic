import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';

// todo: kke: Use here User service!!!!
@Injectable()
export class AdminService {
  public API_URL = 'http://localhost:3010/api';
  constructor(private http: HttpClient) { }


}
