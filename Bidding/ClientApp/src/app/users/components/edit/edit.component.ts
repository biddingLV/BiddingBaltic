// angular
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

// 3rd lib
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';

// internal
import { UsersService } from '../../services/users.service';
import { NotificationsService, FormService } from '../../../core';


@Component({
  selector: 'app-edit-user',
  templateUrl: './edit.component.html',
  styles: []
})
export class UsersEditComponent implements OnInit {
  // info from parent component
  userId: number;
  userFirstName: string;
  userLastName: string;

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

  disableButton: boolean = true;

  // convenience getter for easy access to form fields
  get f() { return this.userEditForm.controls; }

  constructor(
    public bsModalRef: BsModalRef,
    private userApi: UsersService,
    private notification: NotificationsService,
    private fb: FormBuilder,
    private formService: FormService,
    private modalService: BsModalService
  ) { }

  ngOnInit(): void {
    this.buildForm();
  }

  onSubmit(): void {
    this.submitted = true;
    let deleteSuccess: boolean;

    // mark all fields as touched
    this.formService.markFormGroupTouched(this.userEditForm);

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
      this.formErrors = this.formService.validateForm(this.userEditForm, this.formErrors, false);
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

  private buildForm(): void {
    this.userEditForm = this.fb.group({
      usersFirstName: [this.userFirstName, [
        Validators.required,
        Validators.maxLength(100)
      ]],
      usersLastName: [this.userLastName, [
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
      this.formErrors = this.formService.validateForm(this.userEditForm, this.formErrors, true);
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
