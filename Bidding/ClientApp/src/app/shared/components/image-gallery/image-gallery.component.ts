// angular
import { Component, OnInit, Input } from "@angular/core";

// 3rd lib
import {
  NgxGalleryOptions,
  NgxGalleryImage,
  NgxGalleryAnimation
} from "ngx-gallery";

@Component({
  selector: "app-image-gallery",
  templateUrl: "./image-gallery.component.html"
})
export class ImageGalleryComponent implements OnInit {
  // note: kke: Get rid of font awesome css for Icons! Not needed!
  @Input() auctionImageUrls: string[];

  galleryOptions: NgxGalleryOptions[] = [];
  galleryImages: NgxGalleryImage[] = [];

  constructor() {
    this.galleryOptions = [
      {
        width: "100%",
        height: "100%",
        thumbnailsColumns: 2,
        imageAnimation: NgxGalleryAnimation.Slide
        // imageAutoPlay: true, // todo: kke: what is this?
        // imageAutoPlayPauseOnHover: true, // todo: kke: what is this?
        // previewAutoPlay: true, // todo: kke: what is this?
        // previewAutoPlayPauseOnHover: true // todo: kke: what is this?
      },
      // max-width 800
      {
        breakpoint: 800,
        width: "100%",
        height: "600px",
        imagePercent: 80,
        thumbnailsPercent: 20,
        thumbnailsMargin: 20,
        thumbnailMargin: 20
      },
      // max-width 400
      {
        breakpoint: 400,
        preview: false
      }
    ];
  }

  ngOnInit(): void {
    this.auctionImageUrls.forEach(item => {
      var image = {
        small: item,
        medium: item,
        big: item
      };

      this.galleryImages.push(image);
    });
  }
}
