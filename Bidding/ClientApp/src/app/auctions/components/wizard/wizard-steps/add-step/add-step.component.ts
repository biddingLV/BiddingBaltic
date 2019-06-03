// angular
import { Component, OnInit, Input, Output, EventEmitter, SimpleChanges } from '@angular/core';
import { FormGroup } from '@angular/forms';

// internal
import { CategoryConstants } from 'ClientApp/src/app/core/constants';


@Component({
  selector: 'app-auction-add-add-wizard-step',
  templateUrl: './add-step.component.html'
})
export class AuctionAddAddWizardStepComponent implements OnInit {
  @Input() selectedTopCategoryId: number;
  @Input() selectedSubCategoryId: number;

  @Output() emitAddWizardStep = new EventEmitter<boolean>();
  @Output() returnAddWizardStepForm = new EventEmitter<FormGroup>();

  categoryConstants = CategoryConstants;

  constructor() { }

  ngOnInit(): void { }

  ngOnChanges(changes: SimpleChanges): void {
    for (const property in changes) {
      switch (!changes[property].firstChange && property) {
        case 'selectedTopCategoryId':
          this.selectedTopCategoryId = changes[property].currentValue;
          break;
        default:
          break;
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
}
