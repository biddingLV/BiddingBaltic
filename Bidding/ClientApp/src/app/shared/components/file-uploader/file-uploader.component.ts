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

  constructor(
    private http: HttpClient,
    private notification: NotificationsService,
    private exception: ExceptionsService
  ) { }

  upload(files: File[]): void {
    console.log('files', files)
    // if (files.length === 0) {
    //   return;
    // }

    // this.fileName = files[0].name;
    // this.fileIsUploaded = false;

    // const formData = new FormData();
    // formData.append(files[0].name, files[0]);

    // const uploadRequest = new HttpRequest('POST', 'api/imagestorage/upload', formData, {
    //   reportProgress: true,
    // });

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

  remove(): void {
    this.fileIsUploaded = false;
    this.fileName = '';
    this.fileRemoved.emit();
  }
}
