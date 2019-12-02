// angular
import { Component, OnInit, OnDestroy } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

// 3rd lib
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { Subscription } from "rxjs";

// internal
import { UsersService } from "../../services/users.service";
import { NotificationsService } from "ClientApp/src/app/core/services/notifications/notifications.service";
import { FormService } from "ClientApp/src/app/core/services/form/form.service";
import { UserDetailsResponseModel } from "../../models/details/user-details-response.model";
import { UserEditRequestModel } from "../../models/edit/user-edit-request.model";

@Component({
  selector: "app-edit-user",
  templateUrl: "./edit.component.html"
})
export class UserEditComponent implements OnInit, OnDestroy {
  /** Passed from parent component */
  selectedUserId: number;

  // details
  editSubscription: Subscription;

  // component
  editForm: FormGroup;
  submitted = false;
  editRequestModel: UserEditRequestModel;

  // template
  formErrors = {
    userFirstName: "",
    userLastName: "",
    userPhone: ""
  };

  details: UserDetailsResponseModel;

  /** Convenience getter for easy access to form fields */
  get f() {
    return this.editForm.controls;
  }

  constructor(
    public bsModalRef: BsModalRef,
    private usersService: UsersService,
    private notificationService: NotificationsService,
    private formBuilder: FormBuilder,
    private internalFormService: FormService,
    private externalModalService: BsModalService
  ) {}

  ngOnInit(): void {
    this.loadUserDetails();
  }

  onSubmit(): void {
    this.submitted = true;

    this.internalFormService.markFormGroupTouched(this.editForm);

    if (this.editForm.valid) {
      this.setEditRequestValues();
      this.makeRequest();
    } else {
      this.formErrors = this.internalFormService.validateForm(
        this.editForm,
        this.formErrors,
        false
      );
    }

    if (this.editForm.invalid) {
      return;
    }
  }

  ngOnDestroy(): void {
    if (this.editSubscription) {
      this.editSubscription.unsubscribe();
    }
  }

  private loadUserDetails(): void {
    this.editSubscription = this.usersService
      .getUserDetailsForEdit$(this.selectedUserId)
      .subscribe(
        (response: UserDetailsResponseModel) => {
          this.details = response;
          this.buildForm();
        },
        (error: string) => this.notificationService.error(error)
      );
  }

  private buildForm(): void {
    this.editForm = this.formBuilder.group({
      userFirstName: [
        this.details.firstName,
        [Validators.required, Validators.maxLength(50)]
      ],
      userLastName: [
        this.details.lastName,
        [Validators.required, Validators.maxLength(50)]
      ],
      userEmail: [{ value: this.details.email, disabled: true }, []],
      userPhone: [this.details.phone, [Validators.maxLength(100)]]
    });
  }

  private setEditRequestValues(): void {
    this.editRequestModel = {
      userId: this.selectedUserId,
      firstName: this.editForm.value.userFirstName,
      lastName: this.editForm.value.userLastName,
      phone: this.editForm.value.userPhone
    };
  }

  private makeRequest(): void {
    this.editSubscription = this.usersService
      .editUser$(this.editRequestModel)
      .subscribe(
        (editSuccess: boolean) => {
          if (editSuccess) {
            this.notificationService.success("User successfully updated.");
            this.editForm.reset();
            this.bsModalRef.hide();
            this.externalModalService.setDismissReason("Update");
          } else {
            this.notificationService.error("Could not update user.");
          }
        },
        (error: string) => this.notificationService.error(error)
      );
  }
}
