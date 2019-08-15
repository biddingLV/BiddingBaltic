// angular
import { Component, OnInit, EventEmitter, Output } from "@angular/core";

@Component({
  selector: "app-home-sign-up-button",
  templateUrl: "./home-sign-up-button.component.html"
})
export class HomeSignUpButtonComponent implements OnInit {
  @Output() signInChange = new EventEmitter<boolean>();

  constructor() {}

  ngOnInit(): void {}

  onSignInClick(): void {
    this.signInChange.emit(true);
  }
}
