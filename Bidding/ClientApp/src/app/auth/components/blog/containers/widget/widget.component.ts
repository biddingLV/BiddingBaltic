import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { BlogService } from '../../services/blog.service';
import { IBlogWidgetResponse } from '../../models/widget/blog-widget-response.model';
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';

@Component({
  selector: 'app-blog-widget',
  templateUrl: './widget.component.html',
  styleUrls: ['./widget.component.scss']
})
export class BlogWidgetComponent implements OnInit {
  public dummyTitle = 'to do';
  // API
  public blogSubscription: Subscription;
  public posts: IBlogWidgetResponse;

  constructor(private blogApi: BlogService, private notification: NotificationsService) { }

  ngOnInit() {
  }
}
