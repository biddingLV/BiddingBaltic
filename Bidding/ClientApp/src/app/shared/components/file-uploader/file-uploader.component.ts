// angular
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { HttpClient, HttpRequest, HttpEventType } from '@angular/common/http';

// 3rd lib
import { catchError } from 'rxjs/operators';

// internal
import { NotificationsService } from '../../../core';
import { ExceptionsService } from '../../../core/services/exceptions/exceptions.service';
import { FileUploaderService } from '../../services/file-uploader/file-uploader.service';


@Component({
  selector: 'app-file-uploader',
  templateUrl: './file-uploader.component.html'
})
export class FileUploaderComponent {
  @Output() fileUploaded = new EventEmitter<string>();
  @Output() fileRemoved = new EventEmitter();

  progress = 0;
  fileName = '';
  fileIsUploaded = false;

  urls = [];

  constructor(
    private httpService: HttpClient,
    private notificationService: NotificationsService,
    private exceptionService: ExceptionsService,
    private fileUploaderService: FileUploaderService
  ) { }

  upload(event): void {
    if (event.target.files.length === 0) {
      return;
    }

    if (event.target.files && event.target.files[0]) {
      const filesAmount = event.target.files.length;
      for (let i = 0; i < filesAmount; i++) {
        const fileReader: FileReader = new FileReader();

        fileReader.onload = (event: Event) => {
          // event.target.result; // This is not working - 3/11/2019 TypeScript problem
          this.urls.push(fileReader.result);
        };

        fileReader.readAsDataURL(event.target.files[i]);
      }
    }

    const formData = new FormData();

    for (let i = 0; i < event.target.files.length; i++) {
      formData.append('fileArray', event.target.files[i], event.target.files[i].name);
    }

    this.fileUploaderService.uploadFiles$(formData)
      .subscribe((response: boolean) => {
        if (response) {
          this.notificationService.success('File(s) successfully uploaded.');
        } else {
          this.notificationService.error('Could not upload file(s).');
        }
      },
        (error: string) => this.notificationService.error(error));

    // this.httpService.request(uploadRequest)
    //   .pipe(catchError(this.exceptionService.errorHandler))
    //   .subscribe(event => {
    //     if (event.type === HttpEventType.UploadProgress) {
    //       this.progress = Math.round(100 * event.loaded / event.total);
    //     } else if (event.type === HttpEventType.Response) {
    //       if (event.body !== null) {
    //         this.fileIsUploaded = true;
    //         this.fileUploaded.emit(event.body.toString());
    //       } else {
    //         this.notificationService.error('Could not upload image.');
    //       }
    //     }
    //   },
    //     (error: string) => this.notificationService.error(error)
    //   );
  }

  onItemRemove(index: number): void {
    this.urls.splice(index, 1);
  }
}
