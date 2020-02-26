// angular
import { Component, OnInit, Input } from "@angular/core";

// 3rd lib
import {
  NgxGalleryOptions,
  NgxGalleryImage,
  NgxGalleryAnimation
} from "@kolkov/ngx-gallery";

@Component({
  selector: "app-image-gallery",
  templateUrl: "./image-gallery.component.html"
})
export class ImageGalleryComponent implements OnInit {
  // TODO: kke: Get rid of font awesome css for Icons! Not needed!
  @Input() auctionImageUrls: string[];

  /** Default gallery options */
  galleryOptions: NgxGalleryOptions[] = [
    {
      thumbnailsColumns: 2,
      imageAnimation: NgxGalleryAnimation.Slide,
      width: "100%",
      height: "600px",
      imagePercent: 80,
      breakpoint: 400,
      preview: false
    }
  ];

  galleryImages: NgxGalleryImage[] = [
    {
      small: "../../../../assets/images/non-picture-placeholder.jpg",
      medium: "../../../../assets/images/non-picture-placeholder.jpg",
      big: "../../../../assets/images/non-picture-placeholder.jpg"
    }
  ];

  constructor() {}

  ngOnInit(): void {
    this.handleImages();
  }

  private handleImages(): void {
    if (this.auctionImageUrls.length != 0) {
      // remove default image placeholder
      this.galleryImages = [];

      this.auctionImageUrls.forEach(item => {
        let imageObject = {
          small: item,
          medium: item,
          big: item
        };

        this.galleryImages.push(imageObject);
      });
    }
  }
}
