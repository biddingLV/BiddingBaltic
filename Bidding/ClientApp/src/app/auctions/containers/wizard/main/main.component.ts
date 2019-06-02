// angular
import { Component, OnInit, ViewChild, AfterViewInit, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

// 3rd party
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';
import { startWith } from 'rxjs/operators';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker/bs-datepicker.config';
import * as moment from 'moment-mini';

// internal
import { AuctionsService } from '../../../services/auctions.service';
import { FormService } from 'ClientApp/src/app/core/services/form/form.service';
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';
import { AuctionAddRequest } from '../../../models/add/auction-add-request.model';
import { AuctionFilterModel } from '../../../models/filters/auction-filter.model';
import { SubCategoryFilterModel } from '../../../models/filters/sub-category-filter.model';
import { AuctionFormatModel } from '../../../models/add/auction-format.model';
import { AuctionCreatorModel } from '../../../models/add/auction-creator.model';
import { AuctionStatusModel } from '../../../models/add/auction-status.model';
import { WizardComponent, MovingDirection } from 'angular-archwizard';
import { AuctionAddAddWizardStepComponent } from '../../../components/wizard/wizard-steps/add-step/add-step.component';
import { AuctionAddCategoryWizardStepComponent } from '../../../components/wizard/wizard-steps/category-step/category-step.component';


@Component({
  templateUrl: './main.component.html'
})
export class AuctionAddMainWizardComponent implements OnInit, AfterViewInit {
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

  // API
  auctionAddRequest: AuctionAddRequest;

  selectedTopCategoryId: number;
  selectedSubCategoryId: number;

  /** Category step component */
  @ViewChild(AuctionAddCategoryWizardStepComponent) categoryStep: { categoryStepForm: FormGroup; };

  /** Add step component */
  @ViewChild(AuctionAddAddWizardStepComponent) addStep;

  // convenience getter for easy access to form fields
  get f() { return this.auctionAddForm.controls; }

  bsConfig: Partial<BsDatepickerConfig>;

  steps = [0];
  step = 0;

  categoryStepForm: FormGroup;
  addStepForm: FormGroup;

  constructor(
    private auctionApi: AuctionsService,
    private notification: NotificationsService,
    public bsModalRef: BsModalRef
  ) { }

  ngOnInit(): void {
    this.loadTopAndSubCategories();
  }

  ngAfterViewInit(): void {
    this.categoryStepForm = this.categoryStep.categoryStepForm;

  }

  /** Validates if wizard's category step form is valid */
  moveDirectionCategoryStep = (direction: MovingDirection): boolean => {
    return this.moveDirection(this.categoryStepForm, direction);
  }

  /** Validates if wizard's add step form is valid */
  moveDirectionAddStep = (direction: MovingDirection): boolean => {
    this.addStepForm = this.addStep.addStepForm;
    console.log('this.addStep: ', this.addStep)
    return this.moveDirection(this.addStepForm, direction);
  }

  /** Adds additional add-wizard step to the whole wizard flow */
  addWizardStep(event: boolean) {
    this.step++;
    this.steps.push(this.step);
  }

  onTopCategoryChange(categoryId: number): void {
    this.selectedTopCategoryId = categoryId;
  }

  onSubCategoryChange(typeId: number): void {
    this.selectedSubCategoryId = typeId;
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

  private moveDirection = (formOfStep: FormGroup, direction: MovingDirection): boolean => {
    return direction === MovingDirection.Backwards ? true : formOfStep.valid;
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
