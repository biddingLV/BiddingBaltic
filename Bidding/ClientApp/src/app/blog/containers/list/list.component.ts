import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-blog-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class BlogListComponent implements OnInit {
  public newestBlogPosts = [
    {
      id: 1,
      title: '',
      description: ''
    },
    {
      id: 2,
      title: '',
      description: ''
    },
  ];

  constructor() { }

  public ngOnInit() {

  }

}
