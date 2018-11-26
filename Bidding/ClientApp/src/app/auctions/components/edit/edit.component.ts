import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

// import { FeaturesService } from '../../../services/features/features.service';
// import { Guid } from '../../../../shared/types/guid.model';
// import { IDropdownConfig } from '../../../../shared/models/dropdown.model';
// import { IFeatureEditRequest } from '../../../models/features/edit/feature-edit-request.model';

@Component({
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class AuctionEditComponent implements OnInit {
  // public licenseCode: Guid;
  // public featureId: Guid;
  // // todo: kke: for shared dropdown
  // public featureValue: string;
  // public submitted = false;
  // public ddConfig: IDropdownConfig;
  // public request: IFeatureEditRequest;
  // public values: Object[];
  // public featureName: string;
  // public comment: string;

  constructor(public bsModalRef: BsModalRef
    // , private featureApi: FeaturesService
  ) {

  }

  public ngOnInit() {
    // this.initValueDropdown();
  }

  // public onSubmit(form: NgForm) {
  //   this.submitted = true;
  //   let editSuccess: boolean;

  //   this.request = {
  //     FeatureId: this.featureId,
  //     FeatureValue: form.value.featureValue,
  //     LicenseCode: this.licenseCode,
  //     Comment: form.value.Comment
  //   };

  //   this.featureApi.updateFeature(this.request)
  //     .subscribe((data: boolean) => {
  //       editSuccess = data;
  //       if (editSuccess) {
  //         this.bsModalRef.hide();
  //       } else {
  //         // error message
  //       }
  //     });
  // }

  // private initValueDropdown() {
  //   this.values = Array.from(Array(100)
  //     .keys());
  // }
}
