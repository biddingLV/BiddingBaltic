// angular
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

// 3rd party
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';
import { startWith } from 'rxjs/operators';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker/bs-datepicker.config';
import * as moment from 'moment-mini';

// internal
import { AuctionsService } from '../../services/auctions.service';
import { FormService } from 'ClientApp/src/app/core/services/form/form.service';
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';
import { AuctionAddRequest } from '../../models/add/auction-add-request.model';
import { AuctionFilterModel } from '../../models/filters/auction-filter.model';
import { SubCategoryFilterModel } from '../../models/filters/sub-category-filter.model';
import { AuctionFormatModel } from '../../models/add/auction-format.model';
import { AuctionCreatorModel } from '../../models/add/auction-creator.model';
import { AuctionStatusModel } from '../../models/add/auction-status.model';


@Component({
  templateUrl: './add-wizard.component.html'
})
export class AuctionAddWizardComponent implements OnInit {
  // form
  auctionAddForm: FormGroup;
  auctionAddSub: Subscription;
  submitted = false;
  formErrors = {
    auctionName: '',
    auctionTopCategory: '',
    auctionSubCategory: '',
    auctionStartingPrice: '',
    auctionStartDate: '',
    auctionApplyTillDate: '',
    auctionEndDate: '',
    auctionDescription: '',
    auctionCreator: '',
    auctionFormat: '',
    auctionStatus: ''
  };

  // filters
  categories: AuctionFilterModel;
  auctionTypes: SubCategoryFilterModel[];

  auctionFormats: AuctionFormatModel;
  auctionCreators: AuctionCreatorModel;
  auctionStatuses: AuctionStatusModel;

  /** Date format for dates */
  dateFormat = 'YYYY-MM-DD';

  // API
  auctionAddRequest: AuctionAddRequest;

  // convenience getter for easy access to form fields
  get f() { return this.auctionAddForm.controls; }

  bsConfig: Partial<BsDatepickerConfig>;

  constructor(
    private auctionApi: AuctionsService,
    private notification: NotificationsService,
    private fb: FormBuilder,
    private formService: FormService,
    public bsModalRef: BsModalRef
  ) {
    this.bsConfig = {
      dateInputFormat: 'DD/MM/YYYY',
      showWeekNumbers: true,
      minDate: new Date(2000, 1, 1),
      maxDate: new Date(9999, 12, 31)
    };
  }

  ngOnInit(): void {
    this.loadTopAndSubCategories();
  }

  onTopCategoryChange(categoryIds: number[]): void {
    console.log('categoryIds: ', categoryIds);
  }

  onSubCategoryChange(typeIds: number[]): void {
    console.log('typeIds: ', typeIds);
  }

  onSubmit(): void {
    // this.submitted = true;

    // // mark all fields as touched
    // this.formService.markFormGroupTouched(this.auctionAddForm);

    // if (this.auctionAddForm.valid) {
    //   this.setAddRequest();
    //   this.makeRequest();
    // } else {
    //   this.formErrors = this.formService.validateForm(this.auctionAddForm, this.formErrors, false);
    // }

    // // stop here if form is invalid
    // if (this.auctionAddForm.invalid) {
    //   return;
    // }
  }

  private loadTopAndSubCategories(): void {
    this.auctionAddSub = this.auctionApi.getFilters$()
      .pipe(startWith(new AuctionFilterModel()))
      .subscribe(
        (result: AuctionFilterModel) => {
          this.categories = result;
          this.auctionTypes = result.subCategories;
        },
        (error: string) => this.notification.error(error)
      );
  }

  // private buildForm(): void {
  //   this.auctionAddForm = this.fb.group({
  //     // auctionName: ['', [
  //     //   Validators.maxLength(100),
  //     //   Validators.required
  //     // ]],
  //     auctionTopCategory: ['', [
  //       Validators.required
  //     ]],
  //     auctionSubCategory: [{ value: '', disabled: true }, [
  //       Validators.required
  //     ]] // ,
  //     // auctionStartingPrice: ['', [
  //     //   Validators.required
  //     // ]],
  //     // auctionStartDate: ['', [
  //     //   Validators.required
  //     // ]],
  //     // auctionApplyTillDate: ['', []],
  //     // auctionEndDate: ['', []],
  //     // auctionDescription: ['', []],
  //     // auctionCreator: ['', [
  //     //   Validators.required
  //     // ]],
  //     // auctionFormat: ['', [
  //     //   Validators.required
  //     // ]],
  //     // auctionStatus: ['', [
  //     //   Validators.required
  //     // ]]
  //   });

  // this.loadTopAndSubCategories();
  // this.loadAuctionCreators();
  // this.loadAuctionFormats();
  // this.loadAuctionStatuses();
  // }

  // private loadAuctionCreators(): void {
  //   this.auctionAddSub = this.auctionApi.getAuctionCreators$()
  //     .pipe(startWith(new AuctionCreatorModel()))
  //     .subscribe(
  //       (result: AuctionCreatorModel) => {
  //         this.auctionCreators = result;
  //       },
  //       (error: string) => this.notification.error(error)
  //     );
  // }

  // private loadAuctionFormats(): void {
  //   this.auctionAddSub = this.auctionApi.getAuctionFormats$()
  //     .pipe(startWith(new AuctionFormatModel()))
  //     .subscribe(
  //       (result: AuctionFormatModel) => {
  //         this.auctionFormats = result;
  //       },
  //       (error: string) => this.notification.error(error)
  //     );
  // }

  // private loadAuctionStatuses(): void {
  //   this.auctionAddSub = this.auctionApi.getAuctionStatuses$()
  //     .pipe(startWith(new AuctionStatusModel()))
  //     .subscribe(
  //       (result: AuctionStatusModel) => {
  //         this.auctionStatuses = result;
  //       },
  //       (error: string) => this.notification.error(error)
  //     );
  // }

  // private setAddRequest(): void {
  //   this.auctionAddRequest = {
  //     auctionName: this.auctionAddForm.value.auctionName,
  //     auctionTopCategoryIds: this.auctionAddForm.value.auctionTopCategory,
  //     auctionSubCategoryIds: this.auctionAddForm.value.auctionSubCategory,
  //     auctionStartingPrice: this.auctionAddForm.value.auctionStartingPrice,
  //     auctionStartDate: moment(this.auctionAddForm.value.auctionStartDate).format(this.dateFormat),
  //     auctionApplyTillDate: moment(this.auctionAddForm.value.auctionApplyTillDate).format(this.dateFormat),
  //     auctionEndDate: moment(this.auctionAddForm.value.auctionEndDate).format(this.dateFormat),
  //     auctionDescription: this.auctionAddForm.value.auctionDescription,
  //     auctionCreatorId: this.auctionAddForm.value.auctionCreator,
  //     auctionFormatId: this.auctionAddForm.value.auctionFormat,
  //     auctionStatusId: this.auctionAddForm.value.auctionStatus
  //   };
  // }

  // private makeRequest(): void {
  //   this.auctionApi.addAuction$(this.auctionAddRequest)
  //     .subscribe((response: boolean) => {
  //       if (response) {
  //         this.notification.success('Auction successfully added.');
  //         this.auctionAddForm.reset();
  //         this.bsModalRef.hide();
  //       } else {
  //         this.notification.error('Could not add auction.');
  //       }
  //     },
  //       (error: string) => this.notification.error(error));
  // }
}
