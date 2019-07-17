// angular
import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { Title } from '@angular/platform-browser';

// 3rd lib
import { filter, map, mergeMap } from 'rxjs/operators';
import { environment } from '../environments/environment';

declare var gtag;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  public title = 'app';
  private defaultTitle = 'Bidding Portal';
  hideHeader = true;
  hideFooter = true;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private titleService: Title
  ) {
    const script = document.createElement('script');
    script.async = true;
    script.src = 'https://www.googletagmanager.com/gtm.js?id=' + environment.trackingCode;
    document.head.prepend(script);

    //const navEndEvent$ = router.events.pipe(
    //  filter(e => e instanceof NavigationEnd)
    //);
    //navEndEvent$.subscribe((e: NavigationEnd) => {
    //  gtag('config', 'UA-88448661-2', { 'page_path': e.urlAfterRedirects });
    //});
  }

  ngOnInit(): void {
    this.router.events
      .pipe(
        filter((event) => event instanceof NavigationEnd),
        map(() => this.activatedRoute),
        map((route) => {
          while (route.firstChild) { route = route.firstChild; }
          return route;
        }),
        filter((route) => route.outlet === 'primary'),
        mergeMap((route) => route.data))
      .subscribe((event) => {
        if (event['title'] !== undefined) {
          this.titleService.setTitle(event['title'] + ' - ' + this.defaultTitle);
        } else {
          this.titleService.setTitle(this.defaultTitle);
        }
        if (event['hideHeader'] !== undefined && event['hideHeader'] === true) {
          this.hideHeader = true;
        } else {
          this.hideHeader = false;
        }
        if (event['hideFooter'] !== undefined && event['hideFooter'] === true) {
          this.hideFooter = true;
        } else {
          this.hideFooter = false;
        }
      });
  }
}
