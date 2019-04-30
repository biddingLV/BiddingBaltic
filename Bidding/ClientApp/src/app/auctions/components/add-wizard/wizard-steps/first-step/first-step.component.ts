// angular
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

// internal
import { AuctionFilterModel } from 'ClientApp/src/app/auctions/models/filters/auction-filter.model';
import { SubCategoryFilterModel } from 'ClientApp/src/app/auctions/models/filters/sub-category-filter.model';


@Component({
  selector: 'app-first-auction-add-wizard-step',
  templateUrl: './first-step.component.html'
})
export class AuctionAddFirstWizardStepComponent implements OnInit {
  /** Top-categories with sub-categories */
  @Input() categories: AuctionFilterModel;

  /** Selected auction top-category ids */
  @Output() emitTopCategoryIds = new EventEmitter<number[]>();

  /** Selected auction sub-category ids / types */
  @Output() emitSubCategoryIds = new EventEmitter<number[]>();

  /** Auction sub-categories / types used for template only */
  auctionTypes: SubCategoryFilterModel[];

  /** Form what used in the template */
  firstStepForm: FormGroup;

  /** Form error object */
  formErrors = {
    auctionTopCategory: '',
    auctionSubCategory: ''
  };

  constructor(
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.buildForm();
  }

  /** On top-category change filter out sub-categories based on top-level category id */
  onTopCategoryChange(categoryIds: number[]): void {
    this.emitTopCategoryIds.emit(categoryIds);

    if (categoryIds.length > 0) {
      this.firstStepForm.get('auctionSubCategory').enable();
      this.firstStepForm.get('auctionSubCategory').reset();
      this.auctionTypes = this.categories.subCategories.filter(item => categoryIds.includes(item.categoryId));
    } else {
      this.firstStepForm.get('auctionSubCategory').disable();
      this.auctionTypes = this.categories.subCategories;
    }
  }

  onSubCategoryChange(typeIds: number[]): void {
    this.emitSubCategoryIds.emit(typeIds);
  }

  /** Build auction add first wizard step form object*/
  private buildForm(): void {
    this.firstStepForm = this.fb.group({
      auctionTopCategory: ['',
        [
          Validators.required
        ]
      ],
      auctionSubCategory: [
        { value: '', disabled: true },
        [
          Validators.required
        ]
      ]
    });
  }
}
