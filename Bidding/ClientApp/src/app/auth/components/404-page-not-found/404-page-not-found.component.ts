// angular
import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

// 3rd lib
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

// internal
import { AuthService } from 'ClientApp/src/app/core/services/auth/auth.service';

@Component({
  templateUrl: './404-page-not-found.component.html'
})
export class PageNotFoundComponent {
  url: Observable<string>;

  constructor(
    public auth: AuthService,
    public router: Router,
    public route: ActivatedRoute
  ) {
    this.url = route.url
      .pipe(map(segments => segments.join('/')));
  }
}
