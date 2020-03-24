// angular
import {
  Component,
  EventEmitter,
  Output,
  Input,
  ViewChild,
  ElementRef,
  OnDestroy,
  OnInit
} from "@angular/core";

// 3rd lib
import { Subscription } from "rxjs";

// internal
import { FileUploaderService } from "../../services/file-uploader/file-uploader.service";
import { NotificationsService } from "ClientApp/src/app/core/services/notifications/notifications.service";

const allowedImageExtensions = ".png,.jpg,.jpeg";
const allowedDocumentExtensions = ".doc,.docx,.pdf";

const allowedImageTypes = ["image/png", "image/jpeg"];

@Component({
  selector: "app-file-uploader",
  templateUrl: "./file-uploader.component.html",
  styleUrls: ["./file-uploader.component.scss"]
})
export class FileUploaderComponent implements OnInit, OnDestroy {
  @Input() label = "Pievienot attēlus";
  @Input() fileType = "images";
  @Input() multiple = true;
  @Output() fileChange = new EventEmitter<FormDataEntryValue[]>();

  @ViewChild("fileInput") fileInput: ElementRef;

  // component
  fileSubscription: Subscription;
  selectedFiles = new FormData();

  /** Used in template to not allow incorrect file formats */
  acceptExtensions: string = allowedImageExtensions;

  constructor(
    private notificationService: NotificationsService,
    private fileUploaderService: FileUploaderService
  ) {}

  ngOnInit(): void {
    if (this.fileType == "images") {
      this.acceptExtensions = allowedImageExtensions;
    }

    if (this.fileType == "documents") {
      this.acceptExtensions = allowedDocumentExtensions;
    }
  }

  onFileChange(files: FileList): void {
    if (this.fileType == "images") {
      this.handleImages(files);
    }

    if (this.fileType == "documents") {
      this.handleDocuments(files);
    }

    this.validateUploadedFiles();
    this.fileInput.nativeElement.value = null;
  }

  private handleImages(files: FileList) {
    for (let i = 0; i < files.length; i++) {
      let image = files.item(i);

      if (allowedImageTypes.includes(image.type)) {
        if (this.selectedFiles.has(image.name) == false) {
          this.selectedFiles.append(image.name, image);
        }
      } else {
        this.notificationService.warning(
          "Incorrect file format, you can upload only images!"
        );
      }
    }
  }

  private handleDocuments(files: FileList) {
    for (let i = 0; i < files.length; i++) {
      let document = files.item(i);

      if (this.selectedFiles.has(document.name) == false) {
        this.selectedFiles.append(document.name, document);
      }
    }
  }

  onFileRemove(itemName: string): void {
    this.selectedFiles.delete(itemName);

    this.fileChange.emit(this.populateSelectedFiles());
  }

  ngOnDestroy(): void {
    if (this.fileSubscription) {
      this.fileSubscription.unsubscribe();
    }
  }

  private populateSelectedFiles(): FormDataEntryValue[] {
    const items: FormDataEntryValue[] = [];

    this.selectedFiles.forEach(function(val) {
      items.push(val);
    });

    return items;
  }

  private validateUploadedFiles(): void {
    this.fileSubscription = this.fileUploaderService
      .validateFiles$(this.selectedFiles)
      .subscribe(
        (response: boolean) => {
          if (response) {
            this.fileChange.emit(this.populateSelectedFiles());
          } else {
            this.notificationService.error("Could not validate file(s).");
          }
        },
        (error: string) => this.notificationService.error(error)
      );
  }
}
