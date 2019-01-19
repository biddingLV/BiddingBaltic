// angular
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

// 3rd party
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

// internal
import { AuctionsService } from '../../services/auctions.service';
import { FormService } from 'ClientApp/src/app/core/services/form/form.service';
import { AuthService } from 'ClientApp/src/app/core/services/auth/auth.service';
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';
import { CustomValidators } from 'ClientApp/src/app/core/services/form/custom.validators';
import { AuctionAddRequest } from '../../models/add/auction-add-request.model';

// internal

@Component({
  selector: 'app-auction-add',
  templateUrl: './add.component.html',
  styleUrls: []
})
export class AuctionAddComponent implements OnInit {
  // form
  auctionAddForm: FormGroup;
  submitted = false;
  formErrors = {
    name: '',
    description: '',
    startingPrice: '',
    startDate: '',
    creator: ''
  };

  // API
  auctionAddRequest: AuctionAddRequest;

  // convenience getter for easy access to form fields
  get f() { return this.auctionAddForm.controls; }

  // modals
  bsModalRef: BsModalRef;

  constructor(
    private auctionApi: AuctionsService,
    private notification: NotificationsService,
    private fb: FormBuilder,
    private formService: FormService,
    private authService: AuthService,
    private modalService: BsModalService
  ) {

  }

  ngOnInit() {
    this.buildForm();
  }

  onSubmit() {
    this.submitted = true;
    let addSuccess: boolean;

    // mark all fields as touched
    this.formService.markFormGroupTouched(this.auctionAddForm);

    if (this.auctionAddForm.valid) {
      this.initAddRequest();

      this.auctionApi.addAuction$(this.auctionAddRequest)
        .subscribe((data: boolean) => {
          addSuccess = data;
          if (addSuccess) {
            this.notification.success('User successfully added.');
            this.auctionAddForm.reset();
            this.bsModalRef.hide();
          } else {
            this.notification.error('Could not add user.');
          }
        },
          (error: string) => this.notification.error(error));
    } else {
      this.formErrors = this.formService.validateForm(this.auctionAddForm, this.formErrors, false);
    }

    // stop here if form is invalid
    if (this.auctionAddForm.invalid) {
      return;
    }
  }

  private buildForm() {
    this.auctionAddForm = this.fb.group({
      name: ['', [
        Validators.required,
        Validators.maxLength(100)
      ]],
      description: ['', [
        Validators.required,
        Validators.maxLength(100)
      ]],
      startingPrice: ['', [
        Validators.required,
        Validators.maxLength(100)
      ]],
      startDate: [Date.now(), [
        Validators.maxLength(100)
      ]],
      creator: ['', [
        Validators.required
      ]]
    });

    // on each value change we call the validateForm function
    // We only validate form controls that are dirty, meaning they are touched
    // the result is passed to the formErrors object
    this.auctionAddForm.valueChanges.subscribe((data) => {
      this.formErrors = this.formService.validateForm(this.auctionAddForm, this.formErrors, true);
    });
  }

  private initAddRequest() {
    this.auctionAddRequest = {
      // FirstName: this.userAddForm.value.firstName,
      // LastName: this.userAddForm.value.lastName,
      // SignInEmail: this.userAddForm.value.signInEmail,
      // ContactEmail: this.userAddForm.value.signInEmail,
      // Phone: this.userAddForm.value.phone,
      // RoleId: this.userAddForm.value.roles,
      // Comment: this.userAddForm.value.comment,
      // OrganizationId: this.authService.userInfo.OrganizationId // todo: kke: this is wrong for the partner page!
    };
  }
}
