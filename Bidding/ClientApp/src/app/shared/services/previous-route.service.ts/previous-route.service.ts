import { Injectable } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class PreviousRouteService {
  private previousUrl: string;
  private currentUrl: string;
  private indexUrl: string;

  constructor(private router: Router) {
  }

  public init() {
    this.indexUrl = '/';
    this.currentUrl = this.router.url;
    this.previousUrl = undefined;
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.previousUrl = this.currentUrl;
        this.currentUrl = event.url;
      }
    });
  }

  public getPreviousUrl(): string {
    return this.previousUrl;
  }

  public getIndexUrl(): string {
    return this.indexUrl;
  }

  public navigateToPreviousUrl(defaultUrl?: string) {
    const prev = this.getPreviousUrl();
    if (prev !== undefined) {
      this.router.navigate([prev]);
    } else {
      if (defaultUrl !== undefined) {
        this.router.navigate([defaultUrl]);
      } else {
        this.router.navigate([this.indexUrl]);
      }
    }
  }
}
