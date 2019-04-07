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


@Component({
  templateUrl: './add.component.html',
  styleUrls: []
})
export class AuctionAddComponent implements OnInit {
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
    auctionFormat: ''
  };

  // filters
  filters: AuctionFilterModel;
  auctionTypes: SubCategoryFilterModel[];

  auctionFormats: AuctionFormatModel;
  auctionCreators: AuctionCreatorModel;

  // selected Values
  selectedTopCategoryIds: number[];
  selectedSubCategoryIds: number[];

  /** Date format for dates */
  dateFormat = 'DD/MM/YYYY';

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
    this.buildForm();
  }

  onTopCategoryChange(categoryIds: number[]): void {
    this.selectedTopCategoryIds = categoryIds;

    if (categoryIds.length > 0) {
      this.auctionAddForm.get('auctionSubCategory').enable();
      this.auctionAddForm.get('auctionSubCategory').reset();
      this.auctionTypes = this.filters.subCategories.filter(item => categoryIds.includes(item.categoryId));
    } else {
      this.auctionAddForm.get('auctionSubCategory').disable();
      this.auctionTypes = this.filters.subCategories;
    }
  }

  onSubCategoryChange(typeIds: number[]): void {
    this.selectedSubCategoryIds = typeIds;
  }

  onSubmit(): void {
    this.submitted = true;

    // mark all fields as touched
    this.formService.markFormGroupTouched(this.auctionAddForm);

    if (this.auctionAddForm.valid) {
      this.setAddRequest();
      this.makeRequest();
    } else {
      this.formErrors = this.formService.validateForm(this.auctionAddForm, this.formErrors, false);
    }

    // stop here if form is invalid
    if (this.auctionAddForm.invalid) {
      return;
    }
  }

  private buildForm(): void {
    // todo: kke: check whats required here and max lengths?
    this.auctionAddForm = this.fb.group({
      auctionName: ['', [
        Validators.maxLength(100),
        Validators.required
      ]],
      auctionTopCategory: ['', [
        Validators.required
      ]],
      auctionSubCategory: [{ value: '', disabled: true }, [
        Validators.required
      ]],
      auctionStartingPrice: ['', [
        // Validators.maxLength(100),
        Validators.required
      ]],
      auctionStartDate: ['', [
        Validators.required
      ]],
      auctionApplyTillDate: ['', []],
      auctionEndDate: ['', []],
      auctionDescription: ['', [
        Validators.maxLength(100)
      ]],
      auctionCreator: ['', [
        Validators.required
      ]],
      auctionFormat: ['', [
        Validators.required
      ]]
    });

    this.loadTopAndSubCategories();
    this.loadAuctionCreators();
    this.loadAuctionFormats();
  }

  private loadTopAndSubCategories(): void {
    this.auctionAddSub = this.auctionApi.getFilters$()
      .pipe(startWith(new AuctionFilterModel()))
      .subscribe(
        (result: AuctionFilterModel) => {
          this.filters = result;
          this.auctionTypes = result.subCategories;
        },
        (error: string) => this.notification.error(error)
      );
  }

  private loadAuctionCreators(): void {
    this.auctionAddSub = this.auctionApi.getAuctionCreators$()
      .pipe(startWith(new AuctionCreatorModel()))
      .subscribe(
        (result: AuctionCreatorModel) => {
          this.auctionCreators = result;
        },
        (error: string) => this.notification.error(error)
      );
  }

  private loadAuctionFormats(): void {
    this.auctionAddSub = this.auctionApi.getAuctionFormats$()
      .pipe(startWith(new AuctionFormatModel()))
      .subscribe(
        (result: AuctionFormatModel) => {
          this.auctionFormats = result;
        },
        (error: string) => this.notification.error(error)
      );
  }

  private setAddRequest(): void {
    this.auctionAddRequest = {
      auctionName: this.auctionAddForm.value.auctionName,
      auctionTopCategoryIds: this.auctionAddForm.value.auctionTopCategory,
      auctionSubCategoryIds: this.auctionAddForm.value.auctionSubCategory,
      auctionStartingPrice: this.auctionAddForm.value.auctionStartingPrice,
      auctionStartDate: moment(this.auctionAddForm.value.auctionStartDate).format(this.dateFormat),
      auctionApplyTillDate: moment(this.auctionAddForm.value.auctionApplyTillDate).format(this.dateFormat),
      auctionEndDate: moment(this.auctionAddForm.value.auctionEndDate).format(this.dateFormat),
      auctionDescription: this.auctionAddForm.value.auctionDescription,
      auctionCreatorId: this.auctionAddForm.value.auctionCreator,
      auctionFormatId: this.auctionAddForm.value.auctionFormat
    };
  }

  private makeRequest(): void {
    this.auctionApi.addAuction$(this.auctionAddRequest)
      .subscribe((response: boolean) => {
        if (response) {
          this.notification.success('Auction successfully added.');
          this.auctionAddForm.reset();
          this.bsModalRef.hide();
        } else {
          this.notification.error('Could not add auction.');
        }
      },
        (error: string) => this.notification.error(error));
  }
}