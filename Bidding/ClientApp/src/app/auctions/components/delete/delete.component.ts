import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';


@Component({
  templateUrl: './delete.component.html',
  styleUrls: []
})
export class AuctionDeleteComponent implements OnInit {


  constructor(public bsModalRef: BsModalRef
  ) { }

   ngOnInit() { }

}
