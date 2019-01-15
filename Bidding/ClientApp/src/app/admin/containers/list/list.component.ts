// angular
import { Component, OnInit } from '@angular/core';

// 3rd lib


// internal
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';


@Component({
  templateUrl: './list.component.html',
  styleUrls: []
})
export class AdminListComponent implements OnInit {
  constructor(
    private notification: NotificationsService
  ) { }

  ngOnInit() {

  }

}
