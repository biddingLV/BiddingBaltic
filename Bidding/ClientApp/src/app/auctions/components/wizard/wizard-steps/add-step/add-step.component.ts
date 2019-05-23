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
export class AuctionAddAddWizardStepComponent implements OnInit, OnChanges {
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
    private fb: FormBuilder,
    private notificationService: NotificationsService
  ) { }

  ngOnInit(): void {
    this.buildForm();
  }

  ngOnChanges(changes: SimpleChanges): void {
    for (const property in changes) {
      switch (!changes[property].firstChange && property) {
        case 'selectedTopCategoryId':
          let selectedTopCategoryId: number = changes[property].currentValue;
          console.log('selectedTopCategoryId: ', selectedTopCategoryId)
          this.handleStepTemplate(selectedTopCategoryId);
          break;
        // case 'organizationName':
        //   this.request.companyName = changes[property].currentValue;
        //   this.getLicenses();
        //   break;
        default:
          break;
      }
    }
  }

  handleStepTemplate(selectedTopCategoryId: number) {
    switch (selectedTopCategoryId) {
      case CategoryConstants.VEHICLE_CATEGORY: {
        this.showVehicleTemplate = true;
      }
      case CategoryConstants.ITEM_CATEGORY: {
        this.showItemTemplate = true;
      }
      case CategoryConstants.ESTATE_CATEGORY: {
        this.showEstateTemplate = true;
      }
      default: {
        this.notificationService.error('Wrong top category passed to the component!')
      }
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
