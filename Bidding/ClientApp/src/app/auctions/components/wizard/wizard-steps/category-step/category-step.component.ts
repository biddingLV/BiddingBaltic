// angular
import { Component, OnInit, Input, Output, EventEmitter, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

// 3rd lib
import { Subscription } from 'rxjs';
import { startWith } from 'rxjs/operators';

// internal
import { AuctionFilterModel } from 'ClientApp/src/app/auctions/models/filters/auction-filter.model';
import { SubCategoryFilterModel } from 'ClientApp/src/app/auctions/models/filters/sub-category-filter.model';
import { FormService } from 'ClientApp/src/app/core/services/form/form.service';
import { AuctionFormatModel } from 'ClientApp/src/app/auctions/models/add/auction-format.model';
import { AuctionsService } from 'ClientApp/src/app/auctions/services/auctions.service';
import { NotificationsService } from 'ClientApp/src/app/core';


@Component({
  selector: 'app-auction-add-category-wizard-step',
  templateUrl: './category-step.component.html'
})
export class AuctionAddCategoryWizardStepComponent implements OnInit, OnDestroy {
  /** Top-categories with sub-categories */
  @Input() categories: AuctionFilterModel;

  /** Selected auction top-category id */
  @Output() emitTopCategoryId = new EventEmitter<number>();

  /** Selected auction sub-category id / type */
  @Output() emitSubCategoryId = new EventEmitter<number>();

  auctionAddSubscription: Subscription;

  /** Auction sub-categories / types used for template only */
  auctionTypes: SubCategoryFilterModel[];

  /** Form what used in the template */
  categoryStepForm: FormGroup;

  /** Form error object for template */
  formErrors = {
    auctionTopCategory: '',
    auctionSubCategory: '',
    auctionFormat: ''
  };

  submitted = false;

  auctionFormats: AuctionFormatModel;

  constructor(
    private formBuilder: FormBuilder,
    private internalFormService: FormService,
    private auctionService: AuctionsService,
    private notificationService: NotificationsService,
  ) { }

  ngOnInit(): void {
    this.buildForm();
  }

  /** On top-category change filter out sub-categories based on top-level category id */
  onTopCategoryChange(categoryId: number): void {
    this.emitTopCategoryId.emit(categoryId);

    if (categoryId) {
      this.categoryStepForm.get('auctionSubCategory').enable();
      this.categoryStepForm.get('auctionSubCategory').reset();

      this.auctionTypes = this.categories.subCategories.filter(item => item.categoryId === categoryId);
    } else {
      // todo: kke: I think this is not really needed anymore, because you can only select one cat! cant de-select!
      this.categoryStepForm.get('auctionSubCategory').disable();
      this.auctionTypes = this.categories.subCategories;
    }
  }

  /** Return sub-category id to the parent component */
  onSubCategoryChange(typeId: number): void {
    this.emitSubCategoryId.emit(typeId);
  }

  /** On Next click validate if all the required values are specified */
  onNext(): void {
    this.submitted = true;

    // mark all fields as touched
    this.internalFormService.markFormGroupTouched(this.categoryStepForm);

    if (this.categoryStepForm.valid === false) {
      this.formErrors = this.internalFormService.validateForm(this.categoryStepForm, this.formErrors, false);
    }

    // stop here if form is invalid
    if (this.categoryStepForm.invalid) {
      return;
    }
  }

  /** Avoid memory leaks here by cleaning up after ourselves */
  ngOnDestroy(): void {
    if (this.auctionAddSubscription) {
      this.auctionAddSubscription.unsubscribe();
    }
  }

  /** Build auction add category wizard step form group */
  private buildForm(): void {
    this.categoryStepForm = this.formBuilder.group({
      auctionTopCategory: ['', [Validators.required]],
      auctionSubCategory: [{ value: '', disabled: true }, [Validators.required]],
      auctionFormat: ['', [Validators.required]]
    });

    this.loadAuctionFormats();
  }

  private loadAuctionFormats(): void {
    this.auctionAddSubscription = this.auctionService.getAuctionFormats$()
      .pipe(startWith(new AuctionFormatModel()))
      .subscribe(
        (result: AuctionFormatModel) => {
          this.auctionFormats = result;
        },
        (error: string) => this.notificationService.error(error)
      );
  }
}
