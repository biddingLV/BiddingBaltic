// angular
import { Component, OnInit, ViewChild, AfterViewInit, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

// 3rd party
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';
import { startWith } from 'rxjs/operators';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker/bs-datepicker.config';

// internal
import { AuctionsService } from '../../../services/auctions.service';
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';
import { AuctionAddRequest } from '../../../models/add/auction-add-request.model';
import { AuctionFilterModel } from '../../../models/filters/auction-filter.model';
import { SubCategoryFilterModel } from '../../../models/filters/sub-category-filter.model';
import { MovingDirection } from 'angular-archwizard';
import { AuctionAddCategoryWizardStepComponent } from '../../../components/wizard/wizard-steps/category-step/category-step.component';
import { AuctionAddLastWizardStepComponent } from '../../../components/wizard/wizard-steps/last-step/last-step.component';


@Component({
  templateUrl: './main.component.html'
})
export class AuctionAddMainWizardComponent implements OnInit, AfterViewInit {
  auctionAddSub: Subscription;
  submitted = false;

  // filters
  categories: AuctionFilterModel;
  auctionTypes: SubCategoryFilterModel[];

  // API
  auctionAddRequest: AuctionAddRequest;

  selectedTopCategoryId: number;
  selectedSubCategoryId: number;

  /** Category step component */
  @ViewChild(AuctionAddCategoryWizardStepComponent) categoryStep: { categoryStepForm: FormGroup; };

  /** Last step component */
  @ViewChild(AuctionAddLastWizardStepComponent) lastStep: { lastStepForm: FormGroup; };

  bsConfig: Partial<BsDatepickerConfig>;

  steps = [0];
  step = 0;

  categoryStepForm: FormGroup;
  addStepForm: FormGroup;
  lastStepForm: FormGroup;

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
    if (this.addStepForm !== undefined) {
      return this.moveDirection(this.addStepForm, direction);
    } else {
      return false;
    }
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

  onClickNextReturnForm(form: FormGroup): void {
    this.addStepForm = form;
  }

  onSubmit(submitFlag: boolean): void {
    if (submitFlag) {
      this.lastStepForm = this.lastStep.lastStepForm;
      console.log('this.categoryStepForm: ', this.categoryStepForm)
      console.log('this.addStepForm: ', this.addStepForm)
      console.log('this.lastStepForm: ', this.lastStepForm)
    }
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
  // this.loadTopAndSubCategories();
  // this.loadAuctionCreators();
  // this.loadAuctionFormats();
  // this.loadAuctionStatuses();
  // }

  private setAddRequest(): void {
    // this.auctionAddRequest = {
    //   auctionName: this.auctionAddForm.value.auctionName,
    //   auctionTopCategoryIds: this.auctionAddForm.value.auctionTopCategory,
    //   auctionSubCategoryIds: this.auctionAddForm.value.auctionSubCategory,
    //   auctionStartingPrice: this.auctionAddForm.value.auctionStartingPrice,
    //   auctionStartDate: moment(this.auctionAddForm.value.auctionStartDate).format(this.dateFormat),
    //   auctionApplyTillDate: moment(this.auctionAddForm.value.auctionApplyTillDate).format(this.dateFormat),
    //   auctionEndDate: moment(this.auctionAddForm.value.auctionEndDate).format(this.dateFormat),
    //   auctionDescription: this.auctionAddForm.value.auctionDescription,
    //   auctionCreatorId: this.auctionAddForm.value.auctionCreator,
    //   auctionFormatId: this.auctionAddForm.value.auctionFormat,
    //   auctionStatusId: this.auctionAddForm.value.auctionStatus
    // };
  }

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
