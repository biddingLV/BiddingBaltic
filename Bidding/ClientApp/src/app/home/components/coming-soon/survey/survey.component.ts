import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-coming-soon-survey',
  templateUrl: './survey.component.html',
  styleUrls: ['./survey.component.scss']
})
export class ComingSoonSurveyComponent implements OnInit {

  public submitted = false;

  constructor() { }

  public ngOnInit() { }

  public onSubmit(form: NgForm) {
    console.log(form.value);
    this.submitted = true;
    // let addSuccess: boolean;

    // this.addRequest = {
    //   FeatureId: form.value.featureSelect.FeatureId,
    //   FeatureValue: form.value.featureValue,
    //   LicenseCode: this.licenseCode,
    //   Comment: form.value.Comment
    // };

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
