// angular
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { HttpClient, HttpRequest, HttpEventType } from '@angular/common/http';

// 3rd lib
import { catchError } from 'rxjs/operators';

// internal
import { NotificationsService } from '../../../core';
import { ExceptionsService } from '../../../core/services/exceptions/exceptions.service';


@Component({
  selector: 'file-uploader',
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
    private http: HttpClient,
    private notification: NotificationsService,
    private exception: ExceptionsService
  ) { }

  upload(event): void {
    if (event.target.files.length === 0) {
      return;
    }

    if (event.target.files && event.target.files[0]) {
      var filesAmount = event.target.files.length;
      for (let i = 0; i < filesAmount; i++) {
        const fileReader: FileReader = new FileReader();

        fileReader.onload = (event: Event) => {
          // event.target.result; // This is not working - 3/11/2019 TypeScript problem
          this.urls.push(fileReader.result);
        }

        fileReader.readAsDataURL(event.target.files[i]);
      }
    }

    const formData = new FormData();

    for (let i = 0; i < event.target.files.length; i++) {
      formData.append('fileArray', event.target.files[i], event.target.files[i].name);
    }

    const uploadRequest = new HttpRequest('POST', 'api/fileUploader/upload', formData, {
      reportProgress: true,
    });

    // this.http.request(uploadRequest)
    //   .pipe(catchError(this.exception.errorHandler))
    //   .subscribe(event => {
    //     if (event.type === HttpEventType.UploadProgress) {
    //       this.progress = Math.round(100 * event.loaded / event.total);
    //     } else if (event.type === HttpEventType.Response) {
    //       if (event.body !== null) {
    //         this.fileIsUploaded = true;
    //         this.fileUploaded.emit(event.body.toString());
    //       } else {
    //         this.notification.error('Could not upload image.');
    //       }
    //     }
    //   },
    //     (error: string) => this.notification.error(error)
    //   );
  }

  onItemRemove(index: number): void {
    this.urls.splice(index, 1);
  }
}
