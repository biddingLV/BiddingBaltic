// angular
import { Component, OnInit } from '@angular/core';

// 3rd lib
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from 'ngx-gallery';


@Component({
  selector: 'app-image-gallery',
  templateUrl: './image-gallery.component.html'
})
export class ImageGalleryComponent implements OnInit {
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];

  constructor() { }
  ngOnInit(): void {
    this.galleryOptions = [
      {
        width: '600px',
        height: '400px',
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
        width: '100%',
        height: '600px',
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
        small: 'https://biddinglv.blob.core.windows.net/bidauctionimages-67107ed8-d71f-4130-8787-91c202e8434a/8d566cbe-e027-40f4-af14-440132ab4f8a.jpg',
        medium: 'https://biddinglv.blob.core.windows.net/bidauctionimages-67107ed8-d71f-4130-8787-91c202e8434a/8d566cbe-e027-40f4-af14-440132ab4f8a.jpg',
        big: 'https://biddinglv.blob.core.windows.net/bidauctionimages-67107ed8-d71f-4130-8787-91c202e8434a/8d566cbe-e027-40f4-af14-440132ab4f8a.jpg'
      },
      {
        small: 'https://biddinglv.blob.core.windows.net/bidauctionimages-67107ed8-d71f-4130-8787-91c202e8434a/c07440cd-7f64-42ad-ad92-bfd4b1660ee0.jpg',
        medium: 'https://biddinglv.blob.core.windows.net/bidauctionimages-67107ed8-d71f-4130-8787-91c202e8434a/c07440cd-7f64-42ad-ad92-bfd4b1660ee0.jpg',
        big: 'https://biddinglv.blob.core.windows.net/bidauctionimages-67107ed8-d71f-4130-8787-91c202e8434a/c07440cd-7f64-42ad-ad92-bfd4b1660ee0.jpg'
      },
      {
        small: 'https://biddinglv.blob.core.windows.net/bidauctionimages-67107ed8-d71f-4130-8787-91c202e8434a/e5c60ea7-7243-4f5c-bc9e-f09df28f5b53.jpg',
        medium: 'https://biddinglv.blob.core.windows.net/bidauctionimages-67107ed8-d71f-4130-8787-91c202e8434a/e5c60ea7-7243-4f5c-bc9e-f09df28f5b53.jpg',
        big: 'https://biddinglv.blob.core.windows.net/bidauctionimages-67107ed8-d71f-4130-8787-91c202e8434a/e5c60ea7-7243-4f5c-bc9e-f09df28f5b53.jpg'
      }
    ];
  }
}
