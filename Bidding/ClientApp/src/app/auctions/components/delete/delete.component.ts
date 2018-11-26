import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
// import { NgForm } from '@angular/forms';
// import { Guid } from '../../../../shared/types/guid.model';
// import { IFeatureDeleteRequest } from '../../../models/features/delete/feature-delete-request.model';
// import { FeaturesService } from '../../../services/features/features.service';

@Component({
  templateUrl: './delete.component.html',
  styleUrls: ['./delete.component.scss']
})
export class AuctionDeleteComponent implements OnInit {
  // public licenseCode: Guid;
  // public deleteRequest: IFeatureDeleteRequest;
  // public submitted = false;
  // public selectedFeatures;
  // public comment: string;

  constructor(public bsModalRef: BsModalRef
    // , private featureApi: FeaturesService
  ) { }

  public ngOnInit() { }

  // public onSubmit(form: NgForm) {
  //   this.submitted = true;
  //   this.initDeleteRequest(form);
  //   this.callApi();
  // }

  // // private
  // private initDeleteRequest(form) {
  //   this.deleteRequest = {
  //     FeatureIds: this.filterFeatureIds(),
  //     LicenseCode: this.licenseCode,
  //     Comment: form.value.Comment
  //   };
  // }

  // private filterFeatureIds() {
  //   const ids = [];
  //   this.selectedFeatures.forEach(el => {
  //     ids.push(el.FeatureId);
  //   });

  //   return ids;
  // }

  // private callApi() {
  //   let deleteSuccess: boolean;

  //   this.featureApi.deleteFeatures(this.deleteRequest)
  //     .subscribe((data: boolean) => {
  //       deleteSuccess = data;

  //       if (deleteSuccess) {
  //         this.bsModalRef.hide();
  //       } else {
  //         // error message
  //       }
  //     });
  // }
}
