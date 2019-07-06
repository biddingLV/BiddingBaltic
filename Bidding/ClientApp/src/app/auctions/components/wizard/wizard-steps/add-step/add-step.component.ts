// angular
import { Component, OnInit, Input, Output, EventEmitter, SimpleChanges, OnChanges } from '@angular/core';
import { FormGroup } from '@angular/forms';

// internal
import { CategoryConstants } from 'ClientApp/src/app/core/constants';


@Component({
  selector: 'app-auction-add-add-wizard-step',
  templateUrl: './add-step.component.html'
})
export class AuctionAddAddWizardStepComponent implements OnInit, OnChanges {
  @Input() selectedTopCategoryId: number;
  @Input() selectedSubCategoryId: number;

  @Output() emitAddWizardStep = new EventEmitter<boolean>();
  @Output() returnAddWizardStepForm = new EventEmitter<FormGroup>();

  showVehicleStep = false;
  showItemStep = false;
  showPropertyStep = false;

  constructor() { }

  ngOnInit(): void { }

  ngOnChanges(changes: SimpleChanges): void {
    const topCategoryChange = changes['selectedTopCategoryId'];

    if (topCategoryChange && !topCategoryChange.isFirstChange()) {
      if (topCategoryChange !== undefined) {
        this.handleTopCategory();
      }
    }
  }

  addWizardStep(): void {
    this.emitAddWizardStep.emit(true);
  }

  /** Return form values back to parent component */
  onClickNextReturnForm(form: FormGroup): void {
    this.returnAddWizardStepForm.emit(form);
  }

  private handleTopCategory(): void {
    // todo: kke: how to refactor this?
    if (this.selectedTopCategoryId === CategoryConstants.VEHICLE_CATEGORY) {
      this.showVehicleStep = true;
      this.showItemStep = false;
      this.showPropertyStep = false;

      // fetch here vehicle category
    } else if (this.selectedTopCategoryId === CategoryConstants.ITEM_CATEGORY) {
      this.showVehicleStep = false;
      this.showItemStep = true;
      this.showPropertyStep = false;
    } else {
      this.showVehicleStep = false;
      this.showItemStep = false;
      this.showPropertyStep = true;
    }
  }
}
