// angular
import { Component, EventEmitter, Output } from "@angular/core";

// 3rd lib
import { Subscription } from "rxjs";

// internal
import { FileUploaderService } from "../../services/file-uploader/file-uploader.service";
import { NotificationsService } from "ClientApp/src/app/core/services/notifications/notifications.service";

@Component({
  selector: "app-file-uploader",
  templateUrl: "./file-uploader.component.html",
  styleUrls: ["./file-uploader.component.scss"]
})
export class FileUploaderComponent {
  @Output() fileUploaded = new EventEmitter<string>();
  @Output() fileRemoved = new EventEmitter();

  // component
  fileSubscription: Subscription;

  files = [];

  constructor(
    private notificationService: NotificationsService,
    private fileUploaderService: FileUploaderService
  ) {}

  onUpload(selectedFiles: FileList): void {
    if (selectedFiles.length === 0) {
      return;
    }

    if (selectedFiles && selectedFiles[0]) {
      this.setFilesforUI(selectedFiles);
    }

    const formData = new FormData();

    for (let i = 0; i < selectedFiles.length; i++) {
      formData.append("files", selectedFiles[i], selectedFiles[i].name);
    }

    this.validateUploadedFiles(formData);

    // todo: kke: PASS FORMDATA TO PARENT COMPONENT
    // this.filesUploaded.emit(formData);
  }

  onFileRemove(index: number): void {
    this.files.splice(index, 1);
  }

  ngOnDestroy(): void {
    if (this.fileSubscription) {
      this.fileSubscription.unsubscribe();
    }
  }

  private setFilesforUI(selectedFiles: FileList) {
    const fileCount = selectedFiles.length;

    for (let i = 0; i < fileCount; i++) {
      const fileReader: FileReader = new FileReader();

      fileReader.onload = (event: Event) => {
        // event.target.result; // This is not working - 3/11/2019 TypeScript problem
        this.files.push(fileReader.result);
      };

      fileReader.readAsDataURL(selectedFiles[i]);
    }
  }

  private validateUploadedFiles(formData: FormData): void {
    this.fileSubscription = this.fileUploaderService
      .validateFiles$(formData)
      .subscribe(
        (response: boolean) => {
          if (response) {
            this.notificationService.success("All files passed validations");
          } else {
            this.notificationService.error("Could not validate file(s).");
          }
        },
        (error: string) => this.notificationService.error(error)
      );
  }
}
