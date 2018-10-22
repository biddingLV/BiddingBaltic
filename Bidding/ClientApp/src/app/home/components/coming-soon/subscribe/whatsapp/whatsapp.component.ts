import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { IWhatsAppSubscribeRequest } from '../../../../models/whatsapp-subscribe-request.model';
import { HomeService } from '../../../../services/home.service';
import { NotificationsService } from '../../../../../core';

@Component({
  selector: 'app-subscribe-whatsapp',
  templateUrl: './whatsapp.component.html',
  styleUrls: ['./whatsapp.component.scss']
})
export class SubscribeWhatsappComponent {
  // API
  public whatsAppSubRequest: IWhatsAppSubscribeRequest;

  // form
  public model: any = {};
  public checkboxFlag = true;
  public selectedCategories: string[];

  // form checkboxes
  public checkboxes = [
    { category: 'vehicles', name: 'Transportlīdzekļi', checked: false },
    { category: 'items', name: 'Mantas', checked: false },
    { category: 'companies', name: 'Uzņēmumu iegāde', checked: false },
    { category: 'estate', name: 'Nekustamie īpašumi', checked: false },
    { category: 'brands', name: 'Preču zīmes / domēna vārdi', checked: false }
  ];

  constructor(public bsModalRef: BsModalRef, private homeApi: HomeService, private notification: NotificationsService) { }

  public onSubmit(form: NgForm) {
    // filtered categories
    const selectedCategories = this.filterOutCategories();

    if (this.validateCheckboxes(selectedCategories)) {
      this.initWhatsAppRequest(form, selectedCategories);
      this.submitRequest();
    }
  }

  private filterOutCategories(): string[] {
    return this.checkboxes.filter((category) => category.checked).map(x => x.category);
  }

  private validateCheckboxes(categories: string[]): boolean {
    if (categories && categories.constructor === Array && categories.length !== 0) {
      this.checkboxFlag = true;
      return true;
    } else {
      this.checkboxFlag = false;
      return false;
    }
  }

  private initWhatsAppRequest(form, selectedCategories) {
    this.whatsAppSubRequest = {
      Name: form.value.name,
      Phone: form.value.phone,
      Categories: selectedCategories
    };
  }

  private submitRequest() {
    let subSuccess: boolean;

    this.homeApi
      .whatsAppSubscribe$(this.whatsAppSubRequest)
      .subscribe(
        (data: boolean) => {
          subSuccess = data;

          if (subSuccess) {
            this.notification.success('Esam veiksmīgi reģistrējuši tavu pieteikumu.');
            this.bsModalRef.hide();
          } else {
            this.notification.error('Izskatās ka ir notikusi kļūda, atgriezies vēlāk!');
          }
        },
        (error: string) => this.notification.error(error)
      );
  }
}
