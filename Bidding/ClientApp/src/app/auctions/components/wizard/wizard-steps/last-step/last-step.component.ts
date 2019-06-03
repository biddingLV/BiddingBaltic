// angular
import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, FormArray } from '@angular/forms';

// 3rd lib
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker/bs-datepicker.config';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';
import { startWith } from 'rxjs/operators';

// internal
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';
import { AuctionsService } from 'ClientApp/src/app/auctions/services/auctions.service';
import { AuctionCreatorModel } from 'ClientApp/src/app/auctions/models/add/auction-creator.model';
import { AuctionFormatModel } from 'ClientApp/src/app/auctions/models/add/auction-format.model';
import { AuctionStatusModel } from 'ClientApp/src/app/auctions/models/add/auction-status.model';
import { FormService } from 'ClientApp/src/app/core/services/form/form.service';


@Component({
  selector: 'app-auction-add-last-wizard-step',
  templateUrl: './last-step.component.html'
})
export class AuctionAddLastWizardStepComponent implements OnInit {
  // todo: kke: name this emitter submitAllWizardSteps!
  @Output() formValuesAreValid = new EventEmitter<boolean>();

  auctionAddSub: Subscription;

  /** Form what used in the template */
  lastStepForm: FormGroup;

  /** Form error object */
  formErrors = {
    auctionCreator: '',
    auctionAddress: '',
    auctionCreatorEmail: '',
    auctionCreatorPhone: '',
    auctionFormat: '',
    auctionStartDate: '',
    auctionApplyTillDate: '',
    auctionEndDate: ''
  };

  bsConfig: Partial<BsDatepickerConfig>;

  submitted = false;

  auctionFormats: AuctionFormatModel;
  auctionCreators: AuctionCreatorModel;
  auctionStatuses: AuctionStatusModel;

  /** Convenience getter for easy access to form fields */
  get f() { return this.lastStepForm.controls; }

  constructor(
    private formBuilder: FormBuilder,
    public bsModalRef: BsModalRef,
    private auctionApi: AuctionsService,
    private notification: NotificationsService,
    private internalFormService: FormService
  ) { }

  ngOnInit(): void {
    this.buildForm();
  }

  onSubmit(): void {
    this.submitted = true;

    // mark all fields as touched
    this.internalFormService.markFormGroupTouched(this.lastStepForm);

    if (this.lastStepForm.valid == false) {
      this.formErrors = this.internalFormService.validateForm(this.lastStepForm, this.formErrors, false);
    }

    // stop here if form is invalid
    if (this.lastStepForm.invalid) {
      return;
    }

    // return form values back to parent component
    this.formValuesAreValid.emit(true);
  }

  private buildForm(): void {
    this.lastStepForm = this.formBuilder.group({
      auctionCreator: ['Peteris Priede', []],
      auctionAddress: ['Lazdu iela 13', []],
      auctionCreatorEmail: ['peteris@peteris.gg', []],
      auctionCreatorPhone: ['256565656', []],
      auctionFormat: ['', []],
      auctionStartDate: ['02/06/2019', []],
      auctionApplyTillDate: ['01/07/2019', []],
      auctionEndDate: ['01/07/2019', []]
    });

    // this.loadAuctionCreators();
    this.loadAuctionFormats();
  }

  private loadAuctionCreators(): void {
    this.auctionAddSub = this.auctionApi.getAuctionCreators$()
      .pipe(startWith(new AuctionCreatorModel()))
      .subscribe(
        (result: AuctionCreatorModel) => {
          this.auctionCreators = result;
        },
        (error: string) => this.notification.error(error)
      );
  }

  private loadAuctionFormats(): void {
    this.auctionAddSub = this.auctionApi.getAuctionFormats$()
      .pipe(startWith(new AuctionFormatModel()))
      .subscribe(
        (result: AuctionFormatModel) => {
          this.auctionFormats = result;
        },
        (error: string) => this.notification.error(error)
      );
  }

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
}
