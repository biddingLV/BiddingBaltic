import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { HomeService } from '../../../services/home.service';
import { NotificationsService } from '../../../../core';
import { ISurveyRequest } from '../../../models/survey-request.model';



@Component({
  selector: 'app-coming-soon-survey',
  templateUrl: './survey.component.html',
  styleUrls: ['./survey.component.scss']
})
export class ComingSoonSurveyComponent {
  // API
  public surveyRequest: ISurveyRequest;

  // form
  public model: any = {};
  public categoryFlag = true;
  public optionFlag = true;
  public selectedCategories: string[];

  // category checkboxes
  public categoryBoxes = [
    { category: 'vehicles', name: 'Transportlīdzekļi', checked: false },
    { category: 'items', name: 'Mantas', checked: false },
    { category: 'companies', name: 'Uzņēmumu iegāde', checked: false },
    { category: 'brands', name: 'Preču zīmes / domēna vārdi', checked: false }
  ];

  // newsletter type checkboxes
  public newsLetterBoxes = [
    { category: 'email', name: 'epastā', checked: false },
    { category: 'sms', name: 'sms', checked: false },
    { category: 'call', name: 'zvans', checked: false },
    { category: 'other', name: 'tavs variants', checked: false }
  ];

  constructor(private homeApi: HomeService, private notification: NotificationsService) { }

  public onSubmit(form: NgForm) {
    // filtered categories
    const selectedCategories = this.filterOutCategories();
    const validNewsLetterOptions = this.filterOutNLOptions();

    if (this.validateCheckboxes(selectedCategories, validNewsLetterOptions)) {
      this.initEmailRequest(form, selectedCategories);
      this.submitRequest();
    }
  }

  private filterOutCategories(): string[] {
    return this.categoryBoxes.filter((category) => category.checked).map(x => x.category);
  }

  // newsletter options
  private filterOutNLOptions(): string[] {
    return this.newsLetterBoxes.filter((category) => category.checked).map(y => y.category);
  }

  private validateCheckboxes(categories: string[], nwOptions: string[]): boolean {
    if (
      (categories && categories.constructor === Array && categories.length !== 0) &&
      (nwOptions && nwOptions.constructor === Array && nwOptions.length !== 0)
    ) {
      this.categoryFlag = true;
      this.optionFlag = false;
      return true;
    } else {
      this.categoryFlag = false;
      this.optionFlag = false;
      return false;
    }
  }

  private initEmailRequest(form, selectedCategories) {
    this.surveyRequest = {
      Name: form.value.name
    };
  }

  private submitRequest() {
    console.log(this.surveyRequest);

    let surveySuccess: boolean;

    // this.homeApi.emailSubscribe(this.surveyRequest)
    //   .subscribe(
    //     (data: boolean) => {
    //       subSuccess = data;

    //       if (subSuccess) {
    //         this.notification.success('Succesfully subscribed!');
    //       } else {
    //         this.notification.error('N-Succesfully subscribed!');
    //       }
    //     },
    //     (error: string) => this.notification.error(error)
    //   );
  }
}
