// angular
import { Component, OnInit } from '@angular/core';

// 3rd lib
import { GalleryItem, ImageItem } from '@ngx-gallery/core';

@Component({
  selector: 'app-image-gallery',
  templateUrl: './image-gallery.component.html'
})
export class ImageGalleryComponent implements OnInit {
  images: GalleryItem[];

  constructor() { }

  ngOnInit(): void {
    this.images = [
      new ImageItem({ src: 'https://preview.ibb.co/jrsA6R/img12.jpg', thumb: 'https://preview.ibb.co/jrsA6R/img12.jpg' }),
      new ImageItem({ src: 'https://preview.ibb.co/jrsA6R/img12.jpg', thumb: 'https://preview.ibb.co/jrsA6R/img12.jpg' }),
      new ImageItem({ src: 'https://preview.ibb.co/jrsA6R/img12.jpg', thumb: 'https://preview.ibb.co/jrsA6R/img12.jpg' })
    ];
    

  }
}