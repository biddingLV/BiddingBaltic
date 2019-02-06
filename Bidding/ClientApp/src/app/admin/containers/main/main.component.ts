// angular
import { Component, OnInit } from '@angular/core';

// 3rd lib
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

// internal
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';


@Component({
  templateUrl: './main.component.html',
  styleUrls: []
})
export class AdminMainComponent implements OnInit {
  // table
  selected = [];

  // modals
  bsModalRef: BsModalRef;

  constructor(
    private notification: NotificationsService,
    private modalService: BsModalService
  ) { }

  ngOnInit() {

  }
}
