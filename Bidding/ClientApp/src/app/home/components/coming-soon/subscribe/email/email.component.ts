import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { NgForm } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { IEmailSubscribeRequest } from '../../../../models/email-subscribe-request.model';

declare var grecaptcha: any;

@Component({
  selector: 'app-subscribe-email',
  templateUrl: './email.component.html',
  styleUrls: ['./email.component.scss']
})
export class SubscribeEmailComponent implements OnInit {

  // siteKeyCaptcha: string = '6Lf_tW8UAAAAAK-NSTrp4bMYb-zB-3wKyWf-qOBv';

  constructor(private cdr: ChangeDetectorRef, public bsModalRef: BsModalRef) { }

  public emailSubRequest: IEmailSubscribeRequest;

  model: any = {};

  recaptchaStr = '';

  items = [
    { id: 1, name: 'Name 1', isSelected: false },
    { id: 2, name: 'Name 2', isSelected: false },
    { id: 3, name: 'Name 3', isSelected: false }
  ];

  public ngOnInit() {
    // grecaptcha.render('capcha_element', {
    //   'sitekey': this.siteKeyCaptcha
    // });
    // window['getResponceCapcha'] = this.getResponceCapcha.bind(this);
  }

  public onSubmit(form: NgForm) {
    // let addSuccess: boolean;

    this.emailSubRequest = {
      Name: form.value.name,
      Email: form.value.email
    };

    // todo: this works!
    console.log(this.emailSubRequest);

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

  onLoginSubmit(): void {
  }

  onLoginClick(captchaRef: any): void {
    if (this.recaptchaStr) {
      captchaRef.reset();
    }
    captchaRef.execute();
  }

  public resolved(captchaResponse: string): void {
    this.recaptchaStr = captchaResponse;
    if (this.recaptchaStr) {
      this.onLoginSubmit();
    }
  }

}
