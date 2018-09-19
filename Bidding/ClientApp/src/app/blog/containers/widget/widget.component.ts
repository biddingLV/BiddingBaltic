import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { BlogService } from '../../services/blog.service';
import { NotificationsService } from '../../../core/services/notifications/notifications.service';
import { IBlogWidgetRequest } from '../../models/widget/blog-widget-request.model';
import { IBlogWidgetResponse } from '../../models/widget/blog-widget-response.model';

@Component({
  selector: 'app-blog-widget',
  templateUrl: './widget.component.html',
  styleUrls: ['./widget.component.scss']
})
export class BlogWidgetComponent implements OnInit {
  public dummyTitle = 'to do';



  // API
  public blogSubscription: Subscription;
  private request: IBlogWidgetRequest;
  public posts: IBlogWidgetResponse;

  constructor(private blogApi: BlogService, private notification: NotificationsService) { }

  public ngOnInit() {
    this.getBlogPosts();
  }

  private getBlogPosts(): void {
    this.blogSubscription = this.blogApi.getBlogPosts(this.request)
      .subscribe(
        posts => { this.posts = posts; },
        (error: string) => this.notification.error(error)
      );

    console.log('posts', this.posts);
  }

}
