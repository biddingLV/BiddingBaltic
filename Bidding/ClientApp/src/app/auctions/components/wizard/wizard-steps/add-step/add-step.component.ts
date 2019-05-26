// angular
import { Component, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

// internal
import { CategoryConstants } from 'ClientApp/src/app/core/constants';
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';


@Component({
  selector: 'app-auction-add-add-wizard-step',
  templateUrl: './add-step.component.html'
})
export class AuctionAddAddWizardStepComponent implements OnInit {
  @Input() selectedTopCategoryId: number;
  @Input() selectedSubCategoryId: number;

  @Output() emitAddWizardStep = new EventEmitter<boolean>();

  /** Form what used in the template */
  addStepForm: FormGroup;

  submitted = false;

  /** Form error object */
  formErrors = {
    make: '',
    model: '',
    manufacturingDate: '',
    vehicleRegistrationNumber: '',
    vehicleIdentificationNumber: '',
    vehicleInspectionActive: '',
    power: '',
    engineSize: '',
    fuelType: '',
    transmission: '',
    gearbox: '',
    evaluation: ''
  };

  showVehicleTemplate: boolean;
  showItemTemplate: boolean;
  showEstateTemplate: boolean;

  /** Convenience getter for easy access to form fields */
  get f() { return this.addStepForm.controls; }

  constructor(
    private fb: FormBuilder
  ) { }

  ngOnInit(): void { }

  ngOnChanges(changes: SimpleChanges): void {
    for (const property in changes) {
      switch (!changes[property].firstChange && property) {
        case 'selectedTopCategoryId':
          this.selectedTopCategoryId = changes[property].currentValue;
          this.handleStepTemplate();
          break;
        default:
          break;
      }
    }
  }

  handleStepTemplate() { // todo: kke: naming!
    if (this.selectedTopCategoryId == CategoryConstants.ESTATE_CATEGORY) {
      this.showVehicleTemplate = false;
      this.showItemTemplate = false;
      this.showEstateTemplate = true;
    } else if (this.selectedTopCategoryId == CategoryConstants.ITEM_CATEGORY) {
      this.showVehicleTemplate = false;
      this.showItemTemplate = true;
      this.showEstateTemplate = false;
    } else {
      this.showVehicleTemplate = true;
      this.showItemTemplate = false;
      this.showEstateTemplate = false;
    }
  }

  addWizardStep() {
    this.emitAddWizardStep.emit(true);
  }

  private buildForm(): void {
    this.addStepForm = this.fb.group({
      make: ['', []],
      model: ['', []],
      manufacturingDate: ['', []],
      vehicleRegistrationNumber: ['', []],
      vehicleIdentificationNumber: ['', []],
      vehicleInspectionActive: ['', []],
      power: ['', []],
      engineSize: ['', []],
      fuelType: ['', []],
      transmission: ['', []],
      gearbox: ['', []],
      evaluation: ['', []]
    });
  }
}
