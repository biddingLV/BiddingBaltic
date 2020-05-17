// angular
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

// 3rd lib
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  templateUrl: './404-page-not-found.component.html',
})
export class PageNotFoundComponent {
  url: Observable<string>;

  constructor(route: ActivatedRoute) {
    this.url = route.url.pipe(map((segments) => segments.join('/')));
  }
}
