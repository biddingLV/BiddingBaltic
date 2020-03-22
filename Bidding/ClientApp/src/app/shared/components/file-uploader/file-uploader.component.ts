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
  @Output() fileChange = new EventEmitter<File[]>();

  @ViewChild("fileInput") fileInput: ElementRef;

  // component
  fileSubscription: Subscription;
  formData = new FormData();

  // template
  selectedFiles = [];
  enableImagePreview: boolean = false;

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
    if (files.length === 0) {
      return;
    }

    for (let i = 0; i < files.length; i++) {
      let item = files.item(i);

      if (this.fileType == "images") {
        if (this.handleImages(item, files, i) == false) {
          break;
        }

        this.enableImagePreview = true;
      }

      if (this.fileType == "documents") {
        // note: kke: ATM File.type cant really read file type for word documents,
        // not worth to add this validation client side for documents!
        if (this.selectedFiles.includes(item.name) == false) {
          this.selectedFiles.push(item.name);
        }

        this.enableImagePreview = false;
      }

      if (this.formData.get(item.name) == null) {
        this.formData.append(item.name, item);
      }
    }

    this.formData.forEach((value, key) => {
      console.log(key + " " + value);
    });

    // debugger;

    // NOTE: KKE: Are we using this.selectedFiles OR FormDATA????

    // check if formData lenght ir > 0???
    // if (proceed) {
    //   // this.validateUploadedFiles(formData, files);
    //   this.fileInput.nativeElement.value = null;
    // }
  }

  onFileRemove(item): void {
    debugger;
    this.formData.delete(this.formData.get(item));

    this.formData.forEach((value, key) => {
      console.log(key + " " + value);
    });
    // this.selectedFiles.splice(index, 1);
  }

  ngOnDestroy(): void {
    if (this.fileSubscription) {
      this.fileSubscription.unsubscribe();
    }
  }

  private handleImages(item: File, files: FileList, index: number): boolean {
    if (allowedImageTypes.includes(item.type)) {
      this.handleImagePreview(files, index);

      return true;
    } else {
      this.notificationService.warning(
        "Incorrect file format, you can upload only images!"
      );

      return false;
    }
  }

  private handleImagePreview(files: FileList, index: number) {
    const fileReader: FileReader = new FileReader();

    fileReader.onload = (event: Event) => {
      // note: kke: event.target.result - This is not working - 3/11/2019 TypeScript problem
      if (this.selectedFiles.includes(fileReader.result) == false) {
        this.selectedFiles.push({
          fileName: files[index].name,
          fileContent: fileReader.result
        });
      }
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
