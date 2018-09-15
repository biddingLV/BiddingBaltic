import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { IWhatsAppSubscribeRequest } from '../../../../models/whatsapp-subscribe-request.model';

@Component({
  selector: 'app-subscribe-whatsapp',
  templateUrl: './whatsapp.component.html',
  styleUrls: ['./whatsapp.component.scss']
})
export class SubscribeWhatsappComponent implements OnInit {
  constructor(public bsModalRef: BsModalRef) { }

  public whatsAppSubRequest: IWhatsAppSubscribeRequest;

  model: any = {};

  public ngOnInit() { }

  public onSubmit(form: NgForm) {
    let addSuccess: boolean;

    this.whatsAppSubRequest = {
      Name: form.value.name,
      Phone: form.value.phone
    };

    // todo: this works!
    console.log(this.whatsAppSubRequest);

    // this.featureApi.addFeature(this.addRequest)
    //   .subscribe((data: boolean) => {
    //     addSuccess = data;
    //     if (addSuccess) {
    //       this.toastr.success('License Feature successfully added.', '', { positionClass: 'toast-bottom-right' });
    //       this.bsModalRef.hide();
    //     } else {
    //       this.toastr.error('Could not add license feature.', '', { positionClass: 'toast-bottom-right' })
    //     }
    //   });
  }


}
