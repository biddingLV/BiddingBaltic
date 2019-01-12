import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';


@Component({
  selector: 'admin-add',
  templateUrl: './add.component.html',
  styleUrls: []
})
export class AuctionAddComponent implements OnInit {
  constructor(public bsModalRef: BsModalRef
  ) {

  }

  ngOnInit() {

  }
}
