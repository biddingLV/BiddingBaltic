// angular
import { Component, OnInit, Pipe, PipeTransform } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {  HttpClient,HttpEventType } from '@angular/common/http';
// 3rd party
import { BsModalRef } from 'ngx-bootstrap/modal';
import { DatetimePopupModule } from 'ngx-bootstrap-datetime-popup';
// internal
import { AuctionsService } from '../../services/auctions.service';
import { FormService } from 'ClientApp/src/app/core/services/form/form.service';
import { NotificationsService } from 'ClientApp/src/app/core/services/notifications/notifications.service';
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
    auctionName: '',
    description: '',
    startingPrice: '',
    startDate: '',
    joinDate: '',
    endDarw:'',
    creator: '',
    auctionType: ''
  };
  showPicker = false;
  startDate: Date = null;
  endDate: Date = null;
  tillDate: Date = null;
  showDate = true;
  showTime = true;
  closeButton: any = { show: true, label: 'Aizvērt', cssClass: 'btn btn-sm btn-primary' };

  onTogglePicker() {
      if (this.showPicker === false) {
          this.showPicker = true;
      }
  }

  onValueChange(val: Date) {
      this.startDate = val;
      this.endDate = val;
      this.tillDate = val;
  }

  isValid(): boolean {
    // this function is only here to stop the datepipe from erroring if someone types in value
      return this.startDate && (typeof this.startDate !== 'string') && !isNaN(this.startDate.getTime());

  }
  // todo: kke: example
  // startDate: string;
  // endDate: string;

  // API
  auctionAddRequest: AuctionAddRequest;
  // AuctionType
  auctionType = [{id: 1, type: 'Cenu aptauja'}, {id: 2, type: 'Izsole elektroniski'}, {id: 3, type: 'Izsole klātienē'}];

  // convenience getter for easy access to form fields
  get f() { return this.auctionAddForm.controls; }

  constructor(
    private auctionApi: AuctionsService,
    private notification: NotificationsService,
    private fb: FormBuilder,
    private formService: FormService,
    private http: HttpClient,
    public bsModalRef: BsModalRef
  ) { }
  public inputValidator(event: any) {
    //console.log(event.target.value);
    const pattern = /^[a-zA-Z0-9]*/;   
    //let inputChar = String.fromCharCode(event.charCode)
    if (!pattern.test(event.target.value)) {
      event.target.value = event.target.value.replace(/[^a-zA-Z0-9]/g, "");
      // invalid character, prevent input

    }
  }
  currencyInputChanged(value) {
    var num = value.replace(/[€,]/g, "");
    return Number(num);
  }
  ngOnInit() {
    this.buildForm();

    // TODO: KKE: example
    // this.startDate = ''; //moment().subtract(7, 'days').format('YYYY-MM-DD 00:00:01');
    // this.endDate = '';// moment().format('YYYY-MM-DD 23:59:59');
  }
  // File uploading
  selectedFile = null;
  onFileSelected(event){
    this.selectedFile = <File>event.target.files[0];
  }
  onUpload(){
    const fd = new FormData();
    // fd.append('image',this.selectedFile, this.selectedFile.name);
    // this.http.post('http://localhost:4200',fd,{
    //   reportProgress: true,
    //   observe: 'events'
    // })
    // Continue when dealing with bcknd
    // .subscribe(res => {
    //   if(event.type == HttpEventType.UploadProgress){
    //     console.log('Upload Progress: ' + Math.round(event.loaded / event.total * 100)+ '%');
    //   }else if (event.type == HttpEventType.Response) {
    //     console.log(event);
    //   }
    // });
  }
  onSubmit() {
    this.submitted = true;
    let addSuccess: boolean;

    // mark all fields as touched
    this.formService.markFormGroupTouched(this.auctionAddForm);

    if (this.auctionAddForm.valid) {
      this.setAddRequest();

      this.auctionApi.addAuction$(this.auctionAddRequest)
        .subscribe((data: boolean) => {
          addSuccess = data;
          if (addSuccess) {
            this.notification.success('Auction successfully added.');
            this.auctionAddForm.reset();
            this.bsModalRef.hide();
          } else {
            this.notification.error('Could not add auction.');
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

  // todo: kke: example
  updateRequest(property: string, event) {

  }
  
  private buildForm() {
    this.auctionAddForm = this.fb.group({
      auctionName: ['', [
        Validators.maxLength(100)
      ]],
      description: ['', [
        Validators.maxLength(100)
      ]],
      endDate: [, [
        Validators.maxLength(100)
      ]],
      startingPrice: [, [
        Validators.maxLength(100), Validators.required, 
      ]],
      startDate: ['', [
        Validators.maxLength(100)
      ]],
      tillDate: [, [
        Validators.maxLength(100)
      ]],
      creator: ['', [
      ]],
      auctionType: ['', [
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

  private setAddRequest() {
    this.auctionAddRequest = {
      auctionName: this.auctionAddForm.value.auctionName,
      description: this.auctionAddForm.value.description,
      startingPrice: this.auctionAddForm.value.startingPrice,
      // StartDate: this.auctionAddForm.value.startDate,
      creator: this.auctionAddForm.value.creator
    };
  }
}
