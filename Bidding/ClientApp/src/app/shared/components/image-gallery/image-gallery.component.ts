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
        small: 'assets/images/gallery-images/1-small.jpeg',
        medium: 'assets/images/gallery-images/1-medium.jpeg',
        big: 'assets/images/gallery-images/1-medium.jpeg'
      },
      {
        small: 'assets/images/gallery-images/2-small.jpeg',
        medium: 'assets/images/gallery-images/2-medium.jpeg',
        big: 'assets/images/gallery-images/1-medium.jpeg'
      },
      {
        small: 'assets/images/gallery-images/3-small.jpeg',
        medium: 'assets/images/gallery-images/3-medium.jpeg',
        big: 'assets/images/gallery-images/1-medium.jpeg'
      }
    ];

    // this.images = [
    //   new ImageItem({ src: 'https://preview.ibb.co/jrsA6R/img12.jpg', thumb: 'https://preview.ibb.co/jrsA6R/img12.jpg' }),
    //   new ImageItem({ src: 'https://preview.ibb.co/jrsA6R/img12.jpg', thumb: 'https://preview.ibb.co/jrsA6R/img12.jpg' }),
    //   new ImageItem({ src: 'https://preview.ibb.co/jrsA6R/img12.jpg', thumb: 'https://preview.ibb.co/jrsA6R/img12.jpg' })
    // ];


  }
}
