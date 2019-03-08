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

  upload(listWithFiles: FileList): void {
    // https://www.youtube.com/watch?v=YkvqLNcJz3Y
    console.log('files', listWithFiles)
    if (listWithFiles.length === 0) {
      return;
    }

    const formData = new FormData();

    for (let i = 0; i < listWithFiles.length; i++) {
      formData.append('fileArray', listWithFiles[i], listWithFiles[i].name);
    }

    const uploadRequest = new HttpRequest('POST', 'api/fileUploader/upload', formData, {
      reportProgress: true,
    });

    this.http.request(uploadRequest)
      .pipe(catchError(this.exception.errorHandler))
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress) {
          this.progress = Math.round(100 * event.loaded / event.total);
        } else if (event.type === HttpEventType.Response) {
          if (event.body !== null) {
            this.fileIsUploaded = true;
            this.fileUploaded.emit(event.body.toString());
          } else {
            this.notification.error('Could not upload image.');
          }
        }
      },
        (error: string) => this.notification.error(error)
      );
  }

  remove(): void {
    this.fileIsUploaded = false;
    this.fileName = '';
    this.fileRemoved.emit();
  }
}
