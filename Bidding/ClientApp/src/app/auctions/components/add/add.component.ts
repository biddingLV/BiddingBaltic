// angular
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

// 3rd party
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';
import { startWith } from 'rxjs/operators';

// internal
import { AuctionsService } from '../../services/auctions.service';
import { FormService } from 'ClientApp/src/app/core/services/form/form.service';
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';
import { AuctionAddRequest } from '../../models/add/auction-add-request.model';
import { AuctionFilterModel } from '../../models/filters/auction-filter.model';
import { SubCategoryFilterModel } from '../../models/filters/sub-category-filter.model';


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
    // todo: kke: check and fix naming?
    auctionName: '',
    auctionTopCategory: '',
    auctionSubCategory: '',
    // auctionDescription: '',
    auctionStartingPrice: '',
    // auctionStartDate: '',
    // auctionTillDate: '',
    // auctionEndDate: '',
    // auctionCreator: '',
    // auctionFormat: ''
  };

  // filters
  filters: AuctionFilterModel;
  auctionTypes: SubCategoryFilterModel[];

  // selected Values
  selectedTopCategoryIds: number[];
  selectedSubCategoryIds: number[];

  // API
  auctionAddRequest: AuctionAddRequest;

  // convenience getter for easy access to form fields
  get f() { return this.auctionAddForm.controls; }

  // wip
  showStartPicker: boolean = false;
  // wip

  constructor(
    private auctionApi: AuctionsService,
    private notification: NotificationsService,
    private fb: FormBuilder,
    private formService: FormService,
    public bsModalRef: BsModalRef
  ) { }

  ngOnInit(): void {
    this.buildForm();
  }

  onToggleStartPicker(): void {
    if (!this.showStartPicker) {
      this.showStartPicker = true;
    }
  }

  // onToggleEndPicker(): void {
  //   if (!this.showEndPicker) {
  //     this.showEndPicker = true;
  //   }
  // }

  // onToggleTillPicker(): void {
  //   if (!this.showTillPicker) {
  //     this.showTillPicker = true;
  //   }
  // }

  // onValueChange(changedValue: Date): void {

  //   if ('sakums') {
  //     this.startDate = changedValue;
  //   }
  //   if (!'sakums' && !'lidz' && 'beigas') {
  //     this.endDate = changedValue;
  //   }
  //   if ('lidz' && !'beigas' && !'sakums') {
  //     this.tillDate = changedValue;
  //   }
  // }

  // isValidDate(verifyDate = (this.startDate || this.endDate || this.tillDate)): boolean {
  //   // this function is only here to stop the datepipe from erroring if someone types in value
  //   return verifyDate && (typeof verifyDate !== 'string') && !isNaN(verifyDate.getTime());
  // }

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

  /**
   * Called on form submit
   */
  onSubmit(): void {
    // this.submitted = true;

    // // mark all fields as touched
    // this.formService.markFormGroupTouched(this.auctionAddForm);

    // if (this.auctionAddForm.valid) {
    //   this.setAddRequest();

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
    // } else {
    //   this.formErrors = this.formService.validateForm(this.auctionAddForm, this.formErrors, false);
    // }

    // // stop here if form is invalid
    // if (this.auctionAddForm.invalid) {
    //   return;
    // }
  }

  private buildForm(): void {
    // todo: kke: check whats required here and max lengths?
    this.auctionAddForm = this.fb.group({
      auctionName: ['', [
        // Validators.maxLength(100),
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
      auctionTillDate: ['', []],
      auctionEndDate: ['', []],
      auctionDescription: ['', [
        Validators.maxLength(100)
      ]],
      // auctionCreator: ['', [
      //   Validators.required
      // ]],
      // auctionFormatId: ['1', [
      //   Validators.required
      // ]]
    });

    this.loadTopWithSubCategories();
  }

  private loadTopWithSubCategories(): void {
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

  // private setAddRequest(): void {
  //   this.auctionAddRequest = {
  //     auctionName: this.auctionAddForm.value.auctionName,
  //     auctionTopCategoryId: this.auctionAddForm.value.auctionTopCategoryId,
  //     auctionSubCategoryId: this.auctionAddForm.value.auctionSubCategoryId,
  //     auctionDescription: this.auctionAddForm.value.auctionDescription,
  //     auctionStartingPrice: this.auctionAddForm.value.auctionStartingPrice,
  //     auctionStartDate: this.auctionAddForm.value.auctionStartDate,
  //     auctionEndDate: this.auctionAddForm.value.auctionEndDate,
  //     auctionTillDate: this.auctionAddForm.value.auctionTillDate,
  //     auctionCreator: this.auctionAddForm.value.auctionCreator,
  //     auctionFormatId: this.auctionAddForm.value.auctionFormatId
  //   };
  // }
}