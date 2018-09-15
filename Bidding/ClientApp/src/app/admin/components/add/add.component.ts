import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { NgForm } from '@angular/forms';

// import { FeaturesService } from '../../../services/features/features.service';
// import { Guid } from '../../../../shared/types/guid.model';
// import { IDropdownConfig } from '../../../../shared/models/dropdown.model';
// import { IFeatureAddSelectRequest } from '../../../models/features/add/feature-add-select-request.model';
// import { IFeatureAddSelectResponse } from '../../../models/features/add/feature-add-select-response.model';
// import { IFeatureAddRequest } from '../../../models/features/add/feature-add-request.model';
// import { Observable } from 'rxjs';


@Component({
  selector: 'app-auction-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AuctionAddComponent implements OnInit {
  // public licenseCode: Guid;
  // public featureId: Guid;
  // // todo: kke: for shared dropdown
  // public featureValue: string;
  // public submitted = false;
  // public ddConfig: IDropdownConfig;
  // public values: any[];
  // public Object = Object;

  // API
  // public names: IFeatureAddSelectResponse;
  // public nameRequest: IFeatureAddSelectRequest;
  // public addRequest: IFeatureAddRequest;
  // public response$: Observable<IFeatureAddSelectResponse>;
  // public response: IFeatureAddSelectResponse;

  constructor(public bsModalRef: BsModalRef
    // , private featureApi: FeaturesService
  ) {

  }

  public ngOnInit() {
    // this.loadNameSelect();
    // this.initValueDropdown();
  }

  // public onSubmit(form: NgForm) {
  //   this.submitted = true;
  //   let addSuccess: boolean;

  //   this.addRequest = {
  //     // todo: kke: atm no idea how fix this to be fId not fName from html [value]
  //     FeatureId: form.value.featureName,
  //     FeatureValue: form.value.featureValue,
  //     LicenseCode: this.licenseCode,
  //     Comment: form.value.Comment
  //   };

  //   this.featureApi.addFeature(this.addRequest)
  //     .subscribe((data: boolean) => {
  //       addSuccess = data;
  //       if (addSuccess) {
  //         this.bsModalRef.hide();
  //       } else {
  //         // error message
  //       }
  //     });
  // }

  // // Private
  // private initValueDropdown() {
  //   this.values = Array.from(Array(100)
  //     .keys());
  // }

  // private loadNameSelect(): void {
  //   this.response$ = this.featureApi.getFeatureNames(this.licenseCode);
  //   this.response$.subscribe((names: IFeatureAddSelectResponse) => {
  //     this.names = { ...names };
  //   });
  // }
}
