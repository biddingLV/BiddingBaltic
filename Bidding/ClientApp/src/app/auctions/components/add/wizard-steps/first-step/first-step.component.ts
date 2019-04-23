// angular
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

// 3rd lib
import { startWith, map, filter } from 'rxjs/operators';

// internal
import { AuctionsService } from 'ClientApp/src/app/auctions/services/auctions.service';
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';
import { AuctionFilterModel } from 'ClientApp/src/app/auctions/models/filters/auction-filter.model';
import { Observable } from 'rxjs';
import { SubCategoryFilterModel } from 'ClientApp/src/app/auctions/models/filters/sub-category-filter.model';


@Component({
  selector: 'app-first-step',
  templateUrl: './first-step.component.html'
})
export class FirstStepComponent implements OnInit {
  /** Form what used in the template */
  auctionAddForm: FormGroup;

  /** Form error object */
  formErrors = {
    auctionTopCategory: '',
    auctionSubCategory: ''
  };

  categories$: Observable<AuctionFilterModel>;
  auctionTypes$: Observable<SubCategoryFilterModel[]>;

  constructor(
    private auctionApi: AuctionsService,
    private notification: NotificationsService,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.buildForm();
  }

  onTopCategoryChange(categoryIds: number[]): void {
    if (categoryIds.length > 0) {
      this.auctionAddForm.get('auctionSubCategory').enable();
      this.auctionAddForm.get('auctionSubCategory').reset();
      this.auctionTypes$ =
        this.categories$.pipe(
          map(x => x.subCategories.filter(
            y => { console.log(y); categoryIds.includes(y.categoryId) }))) // this.filters.subCategories.filter(item => categoryIds.includes(item.categoryId));
    } else {
      this.auctionAddForm.get('auctionSubCategory').disable();
      this.auctionTypes$ = this.categories$.pipe(map(x => x.subCategories));
    }
  }

  private buildForm(): void {
    this.auctionAddForm = this.fb.group({
      auctionTopCategory: ['', [
        Validators.required
      ]],
      auctionSubCategory: [{ value: '', disabled: true }, [
        Validators.required
      ]]
    });

    this.initializeTopAndSubCategories();
  }

  private initializeTopAndSubCategories(): void {
    this.categories$ = this.auctionApi.getFilters$().pipe(startWith(new AuctionFilterModel()));
  }
}
