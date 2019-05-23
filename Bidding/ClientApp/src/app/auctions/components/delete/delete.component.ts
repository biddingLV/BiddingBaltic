import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { FormGroup } from '@angular/forms';
import { AuctionsService } from '../../services/auctions.service';


@Component({
  templateUrl: './delete.component.html',
  styleUrls: []
})
export class AuctionDeleteComponent implements OnInit {
  // info from parent component
  auctionId: number;

  // form
  userDeleteForm: FormGroup;
  submitted = false;

  formErrors = {
    auctionId: ''
  };

  // API
  organizationUsersRequest: OrganizationUsersRequest;
  deleteRequest: IUserDeleteRequest;
  organizationUsers$: Observable<OrganizationUsersResponse>;

  // convenience getter for easy access to form fields
  get f() { return this.userDeleteForm.controls; }

  constructor(
    public bsModalRef: BsModalRef,
    private auctionService: AuctionsService,
    private notification: NotificationsService,
    private fb: FormBuilder,
    private formService: FormService,
    private router: Router,
    private modalService: BsModalService
  ) { }

  ngOnInit(): void {
    this.buildForm();
  }

  onSubmit(): void {
    this.submitted = true;

    // mark all fields as touched
    this.formService.markFormGroupTouched(this.userDeleteForm);

    if (this.userDeleteForm.valid) {
      this.makeRequest();
    } else {
      this.formErrors = this.formService.validateForm(this.userDeleteForm, this.formErrors, false);
    }

    // stop here if form is invalid
    if (this.userDeleteForm.invalid) {
      return;
    }
  }

  private buildForm(): void {
    this.userDeleteForm = this.fb.group({
      fullName: [, []]
    });

    // on each value change we call the validateForm function
    // We only validate form controls that are dirty, meaning they are touched
    // the result is passed to the formErrors object
    this.userDeleteForm.valueChanges.subscribe((data) => {
      this.formErrors = this.formService.validateForm(this.userDeleteForm, this.formErrors, true);
    });
  }

  private setupDeleteRequest(): void {
    this.deleteRequest = {
      NewContactPersonId: this.contactPersonId
    };
  }

  private makeRequest(): void {
    this.setupDeleteRequest();

    this.auctionService.deleteAuction$(this.deleteRequest)
      .subscribe((data: boolean) => {
        let deleteSuccess: boolean;

        deleteSuccess = data;
        if (deleteSuccess) {
          this.notification.success('User successfully deleted.');
          this.userDeleteForm.reset();
          this.bsModalRef.hide();
          this.formService.specifyDismissReason(this.modalService);
        } else {
          this.notification.error('Could not delete user.');
        }
      },
        (error: string) => this.notification.error(error));
  }
}
