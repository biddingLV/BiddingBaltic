import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

import { SubscribeEmailComponent } from '../../components/coming-soon/subscribe/email/email.component';
import { SubscribeWhatsappComponent } from '../../components/coming-soon/subscribe/whatsapp/whatsapp.component';


@Component({
  selector: 'app-coming-soon-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ComingSoonListComponent implements OnInit {
  public bsModalRef: BsModalRef;

  constructor(private modalService: BsModalService, public router: Router) { }

  ngOnInit() { }

  openEmail() {
    const initialState = {};

    this.bsModalRef = this.modalService.show(SubscribeEmailComponent, { initialState });
    this.bsModalRef.content.closeBtnName = 'Close';
    this.modalService.onHide.subscribe(resp => { });
  }

  openWhatsapp() {
    const initialState = {};

    this.bsModalRef = this.modalService.show(SubscribeWhatsappComponent, { initialState });
    this.bsModalRef.content.closeBtnName = 'Close';
    this.modalService.onHide.subscribe(resp => { });
  }
}
