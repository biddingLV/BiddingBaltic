// angular
import { Component, OnInit, EventEmitter, Output, Input } from "@angular/core";

@Component({
  selector: "app-sign-in-button",
  templateUrl: "./sign-in-button.component.html",
  styleUrls: ["./sign-in-button.component.scss"]
})
export class SignInButtonComponent implements OnInit {
  @Input() buttonConfig: any;
  @Output() signInChange = new EventEmitter<boolean>();

  constructor() {}

  ngOnInit(): void {}

  onSignInClick(event: boolean): void {
    this.signInChange.emit(event);
  }
}
