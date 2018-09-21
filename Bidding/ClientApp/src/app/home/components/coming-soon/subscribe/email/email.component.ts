import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';

import { BsModalRef } from 'ngx-bootstrap/modal';

import { IEmailSubscribeRequest } from '../../../../models/email-subscribe-request.model';
import { HomeService } from '../../../../services/home.service';
import { NotificationsService } from '../../../../../core';
import { IEmailSubscribeResponse } from '../../../../models/email-subscribe-response.model';

@Component({
  selector: 'app-subscribe-email',
  templateUrl: './email.component.html',
  styleUrls: ['./email.component.scss']
})
export class SubscribeEmailComponent {
  // API
  public emailSubRequest: IEmailSubscribeRequest;

  // form
  public model: any = {};
  public checkboxFlag = true;
  public selectedCategories: string[];

  // form checkboxes
  public checkboxes = [
    { category: 'vehicles', name: 'Transportlīdzekļi', checked: false },
    { category: 'items', name: 'Mantas', checked: false },
    { category: 'companies', name: 'Uzņēmumu iegāde', checked: false },
    { category: 'brands', name: 'Preču zīmes / domēna vārdi', checked: false }
  ];

  constructor(public bsModalRef: BsModalRef, private homeApi: HomeService, private notification: NotificationsService) { }

  public onSubmit(form: NgForm) {
    // todo: kke: validate form values here also!

    // filtered categories
    const selectedCategories = this.filterOutCategories();

    if (this.validateCheckboxes(selectedCategories)) {
      this.initEmailRequest(form, selectedCategories);
      this.submitRequest();
    }
  }

  private filterOutCategories(): string[] {
    return this.checkboxes.filter((category) => category.checked).map(x => x.category);
  }

  private validateCheckboxes(categories: string[]): boolean {
    if (categories && categories.constructor === Array && categories.length === 0) {
      this.checkboxFlag = true;
      return true;
    } else {
      this.checkboxFlag = false;
      return false;
    }
  }

  private initEmailRequest(form, selectedCategories) {
    this.emailSubRequest = {
      Name: form.value.name,
      Email: form.value.email // ,
     //  Categories: ['magic']
    };
  }

  private submitRequest() {
    let subSuccess: boolean;

    this.homeApi.emailSubscribe(this.emailSubRequest)
      .subscribe(
        (data: boolean) => {
          subSuccess = data;

          if (subSuccess) {
            this.notification.success('Succesfully subscribed!');
            this.bsModalRef.hide();
          } else {
            this.notification.error('N-Succesfully subscribed!');
          }
        },
        (error: string) => this.notification.error(error)
      );
  }
}
