// angular
import { Component, OnInit, AfterViewInit } from '@angular/core';


@Component({
  selector: 'app-home-main',
  templateUrl: './main.component.html'
})
export class HomeMainComponent implements OnInit, AfterViewInit {
  constructor() { }

  ngOnInit(): void {

  }

  ngAfterViewInit(): void {
    console.log('init');
  }
}
