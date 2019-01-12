import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { IBlogWidgetResponse } from '../models/widget/blog-widget-response.model';
import { BlogWidgetRequest } from '../models/widget/blog-widget-request.model';
import { Observable } from 'rxjs';


@Injectable()
export class BlogService {

  constructor(private http: HttpClient) { }

  public getBlogPosts(request: BlogWidgetRequest): Observable<IBlogWidgetResponse> {
    const url = '/public';

    const params = new HttpParams({
      fromObject: {
        // SortColumn: request.SortColumn.toString(),
        // SortDirection: request.SortDirection.toString(),
        // Search: request.Search.toString(),
        // IncludeChildren: request.IncludeChildren ? 'true' : 'false',
        // OrgId: request.OrgId.toString(),
        // PageSize: request.PageSize.toString(),
        // Page: request.Page.toString()
      }
    });

    return this.http.get<IBlogWidgetResponse>(url, { params });
  }

  // public ping(): void {
  //   this.message = '';
  //   this.http.get<IApiResponse>(`${this.API_URL}/public`)
  //     .subscribe(
  //       data => this.message = data.message,
  //       error => this.message = error
  //     );
  // }
}
