// angular
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

// internal
import { AuctionFilterModel } from 'ClientApp/src/app/auctions/models/filters/auction-filter.model';
import { SubCategoryFilterModel } from 'ClientApp/src/app/auctions/models/filters/sub-category-filter.model';
import { FormService } from 'ClientApp/src/app/core/services/form/form.service';


@Component({
  selector: 'app-auction-add-first-wizard-step',
  templateUrl: './first-step.component.html'
})
export class AuctionAddFirstWizardStepComponent implements OnInit {
  /** Top-categories with sub-categories */
  @Input() categories: AuctionFilterModel;

  /** Selected auction top-category id */
  @Output() emitTopCategoryId = new EventEmitter<number>();

  /** Selected auction sub-category id / type */
  @Output() emitSubCategoryId = new EventEmitter<number>();

  /** Auction sub-categories / types used for template only */
  auctionTypes: SubCategoryFilterModel[];

  /** Form what used in the template */
  firstStepForm: FormGroup;

  /** Form error object for template */
  formErrors = {
    auctionTopCategory: '',
    auctionSubCategory: ''
  };

  submitted = false;

  constructor(
    private fb: FormBuilder,
    private formService: FormService,
  ) { }

  ngOnInit(): void {
    this.buildForm();
  }

  /** On top-category change filter out sub-categories based on top-level category id */
  onTopCategoryChange(categoryId: number): void {
    this.emitTopCategoryId.emit(categoryId);

    if (categoryId) {
      this.firstStepForm.get('auctionSubCategory').enable();
      this.firstStepForm.get('auctionSubCategory').reset();
      this.auctionTypes = this.categories.subCategories.filter(item => item.categoryId == categoryId);
    } else {
      this.firstStepForm.get('auctionSubCategory').disable();
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
    this.formService.markFormGroupTouched(this.firstStepForm);

    if (this.firstStepForm.valid == false) {
      this.formErrors = this.formService.validateForm(this.firstStepForm, this.formErrors, false);
    }

    // stop here if form is invalid
    if (this.firstStepForm.invalid) {
      return;
    }
  }

  /** Build auction add first wizard step form group */
  private buildForm(): void {
    this.firstStepForm = this.fb.group({
      auctionTopCategory: ['', [Validators.required]],
      auctionSubCategory: [{ value: '', disabled: true }, [Validators.required]]
    });
  }
}
