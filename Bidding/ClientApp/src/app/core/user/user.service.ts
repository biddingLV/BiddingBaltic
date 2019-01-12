// Based on: https://auth0.com/docs/quickstart/spa/angular2/01-login

import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/mergeMap';

interface TxWindow extends Window {
  global: Window;
}

(window as TxWindow).global = window;

@Injectable()
export class UserService {


  constructor(public router: Router) {
  }


}
