import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'admin-edit',
  templateUrl: './edit.component.html',
  styleUrls: []
})
export class AuctionEditComponent implements OnInit {

  constructor(public bsModalRef: BsModalRef
  ) {

  }

  ngOnInit() {
  }

}
