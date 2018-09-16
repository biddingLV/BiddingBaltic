import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { NgForm } from '@angular/forms';

import { BsModalRef } from 'ngx-bootstrap/modal';

import { IEmailSubscribeRequest } from '../../../../models/email-subscribe-request.model';

@Component({
  selector: 'app-subscribe-email',
  templateUrl: './email.component.html',
  styleUrls: ['./email.component.scss']
})
export class SubscribeEmailComponent implements OnInit {

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

  constructor(public bsModalRef: BsModalRef) { }

  public ngOnInit() { }

  public onSubmit(form: NgForm) {


    let addSuccess: boolean;

    // const selectedCategories = this.checkboxes.filter((category) => category.checked);

    // const submitFlag = this.validateCheckboxes(selectedCategories);

    this.emailSubRequest = {
      Name: form.value.name,
      Email: form.value.email,
      Categories: this.selectedCategories
    };

    console.log(this.emailSubRequest);
    // if (submitFlag) {
    //   console.log('ok - submit');
    //   // this.featureApi.addFeature(this.addRequest)
    //   //   .subscribe((data: boolean) => {
    //   //     addSuccess = data;
    //   //     if (addSuccess) {
    //   //       this.toastr.success('License Feature successfully added.', '', { positionClass: 'toast-bottom-right' });
    //   //       this.bsModalRef.hide();
    //   //     } else {
    //   //       this.toastr.error('Could not add license feature.', '', { positionClass: 'toast-bottom-right' })
    //   //     }
    //   //   });
    // }
  }

  private validateCheckboxes(selectedCategories: string[]): boolean {
    if (selectedCategories.length > 0) {
      this.checkboxFlag = true;
      return true;
    } else {
      this.checkboxFlag = false;
      return false;
    }
  }
}
