import { Component, OnInit, EventEmitter, Output } from "@angular/core";

@Component({
  selector: "app-auction-file-uploader",
  templateUrl: "./auction-file-uploader.component.html",
  styleUrls: ["./auction-file-uploader.component.scss"]
})
export class AuctionFileUploaderComponent implements OnInit {
  @Output() imageChange = new EventEmitter<FormDataEntryValue[]>();
  @Output() fileChange = new EventEmitter<FormDataEntryValue[]>();

  constructor() {}

  ngOnInit() {}

  onImageChange(images: FormDataEntryValue[]) {
    this.imageChange.emit(images);
  }

  onFileChange(documents: FormDataEntryValue[]) {
    this.fileChange.emit(documents);
  }
}
