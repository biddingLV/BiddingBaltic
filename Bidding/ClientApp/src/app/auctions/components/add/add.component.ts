// angular
import { Component, OnInit, Pipe, PipeTransform } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient, HttpEventType } from '@angular/common/http';

// 3rd party
import { BsModalRef } from 'ngx-bootstrap/modal';
import { DatetimePopupModule } from 'ngx-bootstrap-datetime-popup';
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
  selector: 'app-auction-add',
  templateUrl: './add.component.html',
  styleUrls: []
})
export class AuctionAddComponent implements OnInit {
  // form
  auctionAddForm: FormGroup;
  filtersSub: Subscription;
  submitted = false;
  // todo: jrb: fix  naming and add missing ones!
  formErrors = {
    auctionsName: '',
    auctionsDescription: '',
    auctionsStartingPrice: '',
    auctionsStartDate: '',
    joinDate: '',
    endDarw: '',
    creator: '',
    auctionType: ''
  };

  // filters
  filters: AuctionFilterModel;
  auctionTypes: SubCategoryFilterModel[];

  // used to pass selected filter values to the auction list component
  selectedCategoryIds: number[];
  selectedTypeIds: number[];

  showStartPicker = false;
  showEndPicker = false;
  showTillPicker = false;

  startDate: Date = new Date();
  endDate: Date = new Date();
  tillDate: Date = new Date();

  showDate = true;
  showTime = true;

  showItemCondition = false;
  showPropertyCondition = false;

  closeButton: any = { show: true, label: 'Aizvērt', cssClass: 'btn btn-sm btn-primary' };

  // API
  auctionAddRequest: AuctionAddRequest;

  // AuctionType
  auctionType = [{ id: 1, type: 'Cenu aptauja' }, { id: 2, type: 'Izsole elektroniski' }, { id: 3, type: 'Izsole klātienē' }];

  // AuctionItemCondition
  auctionItemCondition = [{ id: 1, contype: 'Lietota' }, { id: 2, contype: 'Jauna' }];

  // AuctionPropertyCondition
  auctionPropertyCondition = [{ id: 1, proptype: 'Apdzīvots' }, { id: 2, proptype: 'Neapdzīvots' }, { id: 3, proptype: 'Nepieciešams remonts' }];

  // File uploading
  selectedFile = null;

  // convenience getter for easy access to form fields
  get f() { return this.auctionAddForm.controls; }

  constructor(
    private auctionApi: AuctionsService,
    private notification: NotificationsService,
    private fb: FormBuilder,
    private formService: FormService,
    private http: HttpClient,
    public bsModalRef: BsModalRef
  ) { }

  // public inputValidator(event: any) {
  //   //console.log(event.target.value);
  //   const pattern = /^[a-zA-Z0-9]*/;   
  //   //let inputChar = String.fromCharCode(event.charCode)
  //   if (!pattern.test(event.target.value)) {
  //     event.target.value = event.target.value.replace(/[^a-zA-Z0-9]/g, "");
  //     // invalid character, prevent input

  //   }
  // }
  // item = { startingPrice: 1 };
  // onStartingPriceChange(n: string) {
  //   var num = n.replace(/[€,]/g, "");
  //   this.item.startingPrice = Number(num);
  // }
  // currencyInputChanged(value) {
  //   var num = value.replace(/[€,]/g, "");
  //   return Number(num);
  // }

  ngOnInit(): void {
    this.buildForm();
    this.loadFilters();
  }

  onToggleStartPicker() {
    if (this.showStartPicker === false) {
      this.showStartPicker = true;
    }
  }

  onToggleEndPicker() {
    if (this.showEndPicker === false) {
      this.showEndPicker = true;
    }
  }

  onToggleTillPicker() {
    if (this.showTillPicker === false) {
      this.showTillPicker = true;
    }
  }

  onStartValueChange(val: Date) {
    this.startDate = val;
    // this.endDate = val;
    // this.tillDate = val;
  }
  onEndValueChange(val: Date) {
    this.endDate = val;
    // this.tillDate = val;
  }
  onTillValueChange(val: Date) {
    this.tillDate = val;
  }

  isValidStartDate(): boolean {
    // this function is only here to stop the datepipe from erroring if someone types in value
    return this.startDate && (typeof this.startDate !== 'string') && !isNaN(this.startDate.getTime());
  }

  isValidEndDate(): boolean {
    // this function is only here to stop the datepipe from erroring if someone types in value
    return this.endDate && (typeof this.endDate !== 'string') && !isNaN(this.endDate.getTime());
  }

  isValidTillDate(): boolean {
    // this function is only here to stop the datepipe from erroring if someone types in value
    return this.tillDate && (typeof this.tillDate !== 'string') && !isNaN(this.tillDate.getTime());
  }

  // on top category change - select
  onCategoryChange(categoryIds: number[]): void {
    this.selectedCategoryIds = categoryIds;

    if (categoryIds.length > 0) {
      // filter out based on selected category ids
      this.auctionTypes = this.filters.subCategories.filter(item => { return categoryIds.includes(item.categoryId) });
    }
    else {
      // nothing selected show the full list
      this.auctionTypes = this.filters.subCategories;
    }
  }

  onTypeChange(typeIds: number[]): void {
    this.selectedTypeIds = typeIds;
  }

  /**
   * Called on form submit
   */
  onSubmit(): void {
    this.submitted = true;

    // mark all fields as touched
    this.formService.markFormGroupTouched(this.auctionAddForm);

    if (this.auctionAddForm.valid) {
      this.setAddRequest();

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
    } else {
      this.formErrors = this.formService.validateForm(this.auctionAddForm, this.formErrors, false);
    }

    // stop here if form is invalid
    if (this.auctionAddForm.invalid) {
      return;
    }
  }

  private buildForm() {
    this.auctionAddForm = this.fb.group({

      auctionName: ['', [
        Validators.maxLength(100)
      ]],
      description: ['', [
        Validators.maxLength(100)
      ]],
      endDate: [, [
        Validators.maxLength(100)
      ]],
      startingPrice: [, [
        Validators.maxLength(100), Validators.required, Validators.pattern('€[0-9]')
      ]],
      startDate: ['', [
        Validators.maxLength(100)
      ]],
      tillDate: [, [
        Validators.maxLength(100)
      ]],
      creator: ['', [
      ]],
      auctionType: ['', [
        Validators.required
      ]]
    });

    // on each value change we call the validateForm function
    // We only validate form controls that are dirty, meaning they are touched
    // the result is passed to the formErrors object
    this.auctionAddForm.valueChanges.subscribe((data) => {
      this.formErrors = this.formService.validateForm(this.auctionAddForm, this.formErrors, true);
    });
  }

  private loadFilters(): void {
    this.filtersSub = this.auctionApi.getFilters$()
      .pipe(startWith(new AuctionFilterModel()))
      .subscribe(
        (result: AuctionFilterModel) => {
          this.filters = result;
          this.auctionTypes = result.subCategories;
        },
        (error: string) => this.notification.error(error)
      );
  }

  private setAddRequest() {
    this.auctionAddRequest = {
      auctionName: this.auctionAddForm.value.auctionName,
      description: this.auctionAddForm.value.description,
      startingPrice: this.auctionAddForm.value.startingPrice,
      // StartDate: this.auctionAddForm.value.startDate,
      creator: this.auctionAddForm.value.creator
    };
  }
}
