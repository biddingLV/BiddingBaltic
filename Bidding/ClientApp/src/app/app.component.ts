// angular
import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { Title } from '@angular/platform-browser';

// 3rd lib
import { filter, map, mergeMap } from 'rxjs/operators';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
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
  ) { }

  ngOnInit() {
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
