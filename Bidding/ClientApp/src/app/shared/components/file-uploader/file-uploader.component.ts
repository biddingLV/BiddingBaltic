// angular
import {
  Component,
  EventEmitter,
  Output,
  Input,
  ViewChild,
  ElementRef
} from "@angular/core";

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
  @Input() label = "Pievienot attēlus";
  @Input() acceptFormats = "*/*";
  @Input() multiple = true;
  @Output() fileChange = new EventEmitter<File[]>();

  @ViewChild("fileInput", { static: false }) fileInput: ElementRef;

  // component
  fileSubscription: Subscription;

  // template
  selectedFiles = [];
  enablePreview: boolean = false;

  constructor(
    private notificationService: NotificationsService,
    private fileUploaderService: FileUploaderService
  ) {}

  onFileChange(files: FileList): void {
    if (files.length === 0) {
      return;
    }

    let proceed: boolean = true;

    const formData = new FormData();

    for (let i = 0; i < files.length; i++) {
      let item = files.item(i);

      formData.append(item.name, item);

      if (this.acceptFormats == "image/*") {
        if (!this.onlyImagesAllowed(item, files, i)) {
          proceed = false;
          break;
        }

        this.enablePreview = true;
      }

      if (this.acceptFormats == "application/pdf") {
        if (!this.onlyPDFAllowed(item)) {
          proceed = false;
          break;
        }
      }
    }

    if (proceed) {
      this.validateUploadedFiles(formData, files);
      this.fileInput.nativeElement.value = null;
    }
  }

  onFileRemove(index: number): void {
    this.selectedFiles.splice(index, 1);
  }

  ngOnDestroy(): void {
    if (this.fileSubscription) {
      this.fileSubscription.unsubscribe();
    }
  }

  private onlyImagesAllowed(item: File, files: FileList, i: number): boolean {
    if (item.type.match(/image\/*/) == null) {
      this.notificationService.warning(
        "Incorrect file format, you can upload only images here!"
      );

      return false;
    } else {
      this.handleImagePreview(files, i);

      return true;
    }
  }

  private onlyPDFAllowed(item: File): boolean {
    if (item.type.match(/application\/pdf/) == null) {
      this.notificationService.warning(
        "Incorrect file format, you can upload only pdf here!"
      );

      return false;
    }

    this.selectedFiles.push(item.name);

    return true;
  }

  private handleImagePreview(files: FileList, i: number) {
    const fileReader: FileReader = new FileReader();

    fileReader.onload = (event: Event) => {
      // note: kke: event.target.result - This is not working - 3/11/2019 TypeScript problem
      this.selectedFiles.push(fileReader.result);
    };

    fileReader.readAsDataURL(files[i]);
  }

  private validateUploadedFiles(formData: FormData, files: FileList): void {
    const records = Array.from(files);

    this.fileSubscription = this.fileUploaderService
      .validateFiles$(formData)
      .subscribe(
        (response: boolean) => {
          if (response) {
            this.notificationService.success("All files passed validations");
            this.fileChange.emit(records);
          } else {
            this.notificationService.error("Could not validate file(s).");
          }
        },
        (error: string) => this.notificationService.error(error)
      );
  }
}
