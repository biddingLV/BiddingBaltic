import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'admin-delete',
  templateUrl: './delete.component.html',
  styleUrls: []
})
export class AuctionDeleteComponent implements OnInit {

  constructor(public bsModalRef: BsModalRef
  ) { }

  ngOnInit() { }
}
