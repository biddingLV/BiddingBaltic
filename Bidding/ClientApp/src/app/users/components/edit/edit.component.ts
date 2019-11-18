// angular
import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

// 3rd lib
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';

// internal
import { UsersService } from '../../services/users.service';
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';
import { FormService } from 'ClientApp/src/app/core/services/form/form.service';


@Component({
  selector: 'app-edit-user',
  templateUrl: './edit.component.html'
})
export class UserEditComponent implements OnInit, OnDestroy {
  /** Passed from parent component */
  selectedUserId: number;

  // details
  userEditSubscription: Subscription;

  // form
  userEditForm: FormGroup;
  userEditFormAtStart: FormGroup;
  submitted = false;

  formErrors = {
    usersFirstName: '',
    usersLastName: ''
  };

  disableButton = true;

  // convenience getter for easy access to form fields
  get f() { return this.userEditForm.controls; }

  constructor(
    public bsModalRef: BsModalRef,
    private usersService: UsersService,
    private notificationService: NotificationsService,
    private formBuilder: FormBuilder,
    private internalFormService: FormService
  ) { }

  ngOnInit(): void {
    // todo: kke: prob we need some flag here to load own profile details
    // || load profile details as an admin looking at another users profile!
    this.loadUserDetails();
  }

  onSubmit(): void {
    this.submitted = true;

    // mark all fields as touched
    this.internalFormService.markFormGroupTouched(this.userEditForm);

    if (this.userEditForm.valid) {
      this.initEditRequest();

      // this.userApi.editUser$(this.editRequest)
      //   .subscribe((data: boolean) => {
      //     deleteSuccess = data;
      //     if (deleteSuccess) {
      //       this.notification.success('User successfully updated.');
      //       this.userEditForm.reset();
      //       this.bsModalRef.hide();
      //       this.formService.specifyDismissReason(this.modalService);
      //     } else {
      //       this.notification.error('Could not update user.');
      //     }
      //   },
      //     (error: string) => this.notification.error(error));
    } else {
      this.formErrors = this.internalFormService.validateForm(this.userEditForm, this.formErrors, false);
    }

    // stop here if form is invalid
    if (this.userEditForm.invalid) {
      return;
    }
  }

  ngOnDestroy(): void {
    if (this.userEditSubscription) {
      this.userEditSubscription.unsubscribe();
    }
  }

  private loadUserDetails(): void {
    this.userEditSubscription = this.usersService
      .getUserDetailsForEdit$(this.selectedUserId)
      .subscribe(
        (response: any) => { this.buildForm(); }, // todo: kke: SPECIFY RESPONSE TYPE!
        (error: string) => this.notificationService.error(error)
      );
  }

  private buildForm(): void {
    this.userEditForm = this.formBuilder.group({
      usersFirstName: ['', [
        Validators.required,
        Validators.maxLength(100)
      ]],
      usersLastName: ['', [
        Validators.required,
        Validators.maxLength(100)
      ]]
    });
  }

  private subscribeToFormChanges(): void {
    //// on each value change we call the validateForm function
    //// We only validate form controls that are dirty, meaning they are touched
    //// the result is passed to the formErrors object
    this.userEditForm.valueChanges.subscribe((data) => {
      // this.formService.hasAFormControlChange(this.userEditFormAtStart, this.userEditForm) ? this.disableButton = false : this.disableButton = true;
      this.formErrors = this.internalFormService.validateForm(this.userEditForm, this.formErrors, true);
    });
  }

  private initEditRequest(): void {
    // this.editRequest = {
    //   FirstName: this.userEditForm.value.usersFirstName,
    //   LastName: this.userEditForm.value.usersLastName,
    //   UserId: this.userId
    // };
  }
}
