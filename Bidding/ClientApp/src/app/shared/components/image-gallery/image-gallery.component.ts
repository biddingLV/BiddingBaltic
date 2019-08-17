// angular
import { Component, OnInit } from "@angular/core";

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
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];

  constructor() {}
  ngOnInit(): void {
    this.galleryOptions = [
      {
        width: "100%",
        height: "100%",
        thumbnailsColumns: 4,
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

    this.galleryImages = [
      {
        small:
          "https://biddinglv.blob.core.windows.net/bidauctionimages-d010eef9-6db7-41ca-9e76-7029f09a6c31/13dab0ae-3641-40d9-ae91-5855a5c6e384.jpg",
        medium:
          "https://biddinglv.blob.core.windows.net/bidauctionimages-d010eef9-6db7-41ca-9e76-7029f09a6c31/13dab0ae-3641-40d9-ae91-5855a5c6e384.jpg",
        big:
          "https://biddinglv.blob.core.windows.net/bidauctionimages-d010eef9-6db7-41ca-9e76-7029f09a6c31/13dab0ae-3641-40d9-ae91-5855a5c6e384.jpg"
      },
      {
        small:
          "https://biddinglv.blob.core.windows.net/bidauctionimages-d010eef9-6db7-41ca-9e76-7029f09a6c31/13dab0ae-3641-40d9-ae91-5855a5c6e384.jpg",
        medium:
          "https://biddinglv.blob.core.windows.net/bidauctionimages-d010eef9-6db7-41ca-9e76-7029f09a6c31/13dab0ae-3641-40d9-ae91-5855a5c6e384.jpg",
        big:
          "https://biddinglv.blob.core.windows.net/bidauctionimages-d010eef9-6db7-41ca-9e76-7029f09a6c31/13dab0ae-3641-40d9-ae91-5855a5c6e384.jpg"
      },
      {
        small:
          "https://biddinglv.blob.core.windows.net/bidauctionimages-d010eef9-6db7-41ca-9e76-7029f09a6c31/13dab0ae-3641-40d9-ae91-5855a5c6e384.jpg",
        medium:
          "https://biddinglv.blob.core.windows.net/bidauctionimages-d010eef9-6db7-41ca-9e76-7029f09a6c31/13dab0ae-3641-40d9-ae91-5855a5c6e384.jpg",
        big:
          "https://biddinglv.blob.core.windows.net/bidauctionimages-d010eef9-6db7-41ca-9e76-7029f09a6c31/13dab0ae-3641-40d9-ae91-5855a5c6e384.jpg"
      },
      {
        small:
          "https://biddinglv.blob.core.windows.net/bidauctionimages-d010eef9-6db7-41ca-9e76-7029f09a6c31/13dab0ae-3641-40d9-ae91-5855a5c6e384.jpg",
        medium:
          "https://biddinglv.blob.core.windows.net/bidauctionimages-d010eef9-6db7-41ca-9e76-7029f09a6c31/13dab0ae-3641-40d9-ae91-5855a5c6e384.jpg",
        big:
          "https://biddinglv.blob.core.windows.net/bidauctionimages-d010eef9-6db7-41ca-9e76-7029f09a6c31/13dab0ae-3641-40d9-ae91-5855a5c6e384.jpg"
      },
      {
        small:
          "https://biddinglv.blob.core.windows.net/bidauctionimages-d010eef9-6db7-41ca-9e76-7029f09a6c31/13dab0ae-3641-40d9-ae91-5855a5c6e384.jpg",
        medium:
          "https://biddinglv.blob.core.windows.net/bidauctionimages-d010eef9-6db7-41ca-9e76-7029f09a6c31/13dab0ae-3641-40d9-ae91-5855a5c6e384.jpg",
        big:
          "https://biddinglv.blob.core.windows.net/bidauctionimages-d010eef9-6db7-41ca-9e76-7029f09a6c31/13dab0ae-3641-40d9-ae91-5855a5c6e384.jpg"
      },
      {
        small:
          "https://biddinglv.blob.core.windows.net/bidauctionimages-d010eef9-6db7-41ca-9e76-7029f09a6c31/13dab0ae-3641-40d9-ae91-5855a5c6e384.jpg",
        medium:
          "https://biddinglv.blob.core.windows.net/bidauctionimages-d010eef9-6db7-41ca-9e76-7029f09a6c31/13dab0ae-3641-40d9-ae91-5855a5c6e384.jpg",
        big:
          "https://biddinglv.blob.core.windows.net/bidauctionimages-d010eef9-6db7-41ca-9e76-7029f09a6c31/13dab0ae-3641-40d9-ae91-5855a5c6e384.jpg"
      },
      {
        small:
          "https://biddinglv.blob.core.windows.net/bidauctionimages-d010eef9-6db7-41ca-9e76-7029f09a6c31/13dab0ae-3641-40d9-ae91-5855a5c6e384.jpg",
        medium:
          "https://biddinglv.blob.core.windows.net/bidauctionimages-d010eef9-6db7-41ca-9e76-7029f09a6c31/13dab0ae-3641-40d9-ae91-5855a5c6e384.jpg",
        big:
          "https://biddinglv.blob.core.windows.net/bidauctionimages-d010eef9-6db7-41ca-9e76-7029f09a6c31/13dab0ae-3641-40d9-ae91-5855a5c6e384.jpg"
      },
      {
        small:
          "https://biddinglv.blob.core.windows.net/bidauctionimages-d010eef9-6db7-41ca-9e76-7029f09a6c31/13dab0ae-3641-40d9-ae91-5855a5c6e384.jpg",
        medium:
          "https://biddinglv.blob.core.windows.net/bidauctionimages-d010eef9-6db7-41ca-9e76-7029f09a6c31/13dab0ae-3641-40d9-ae91-5855a5c6e384.jpg",
        big:
          "https://biddinglv.blob.core.windows.net/bidauctionimages-d010eef9-6db7-41ca-9e76-7029f09a6c31/13dab0ae-3641-40d9-ae91-5855a5c6e384.jpg"
      }
    ];
  }
}
