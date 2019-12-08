import { Component, OnInit, EventEmitter, Output } from "@angular/core";

@Component({
  selector: "app-auction-file-uploader",
  templateUrl: "./auction-file-uploader.component.html",
  styleUrls: ["./auction-file-uploader.component.scss"]
})
export class AuctionFileUploaderComponent implements OnInit {
  @Output() imageChange = new EventEmitter<File[]>();
  @Output() fileChange = new EventEmitter<File[]>();

  constructor() {}

  ngOnInit() {}

  onImageChange(files: File[]) {
    this.imageChange.emit(files);
  }

  onFileChange(files: File[]) {
    this.fileChange.emit(files);
  }
}
