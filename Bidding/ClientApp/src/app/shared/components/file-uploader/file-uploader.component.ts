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

@Component({
  selector: "app-file-uploader",
  templateUrl: "./file-uploader.component.html",
  styleUrls: ["./file-uploader.component.scss"]
})
export class FileUploaderComponent implements OnInit, OnDestroy {
  @Input() label = "Pievienot attēlus";
  @Input() fileType = "images";
  @Input() multiple = true;
  @Output() fileChange = new EventEmitter<File[]>();

  @ViewChild("fileInput") fileInput: ElementRef;

  // component
  fileSubscription: Subscription;

  // template
  selectedFiles = [];
  enablePreview: boolean = false;
  acceptExtensions: string = allowedImageExtensions;

  constructor(
    private notificationService: NotificationsService,
    private fileUploaderService: FileUploaderService
  ) {}

  ngOnInit(): void {
    if (this.fileType != "images") {
      this.acceptExtensions = allowedDocumentExtensions;
    }
  }

  onFileChange(files: FileList): void {
    if (files.length === 0) {
      return;
    }

    let proceed: boolean = true;

    const formData = new FormData();

    for (let i = 0; i < files.length; i++) {
      let item = files.item(i);
      formData.append(item.name, item);

      if (this.fileType == "images") {
        if (!this.handleImages(item, files, i)) {
          proceed = false;
          break;
        }

        this.enablePreview = true;
      }

      if (this.fileType == "documents") {
        if (!this.handleDocuments(item)) {
          proceed = false;
          break;
        }
      }
    }

    if (proceed) {
      // this.validateUploadedFiles(formData, files);
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

  private handleImages(item: File, files: FileList, index: number): boolean {
    const validImageTypes = "image/*";

    if (item.type.match(validImageTypes) == null) {
      this.notificationService.warning(
        "Incorrect file format, you can upload only images here!"
      )
      return false;
    } else {
      this.handleImagePreview(files, index);

      return true;
    }
  }

  private handleDocuments(item: File): boolean {
    const validDocumentFormats = "/application/pdf/";

    if (item.type.match(validDocumentFormats) == null) {
      this.notificationService.warning(
        "Incorrect file format, you can upload only pdf here!"
      );

      return false;
    }

    this.selectedFiles.push(item.name);

    return true;
  }

  private handleImagePreview(files: FileList, index: number) {
    const fileReader: FileReader = new FileReader();

    fileReader.onload = (event: Event) => {
      // note: kke: event.target.result - This is not working - 3/11/2019 TypeScript problem
      this.selectedFiles.push(fileReader.result);
    };

    fileReader.readAsDataURL(files[index]);
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
