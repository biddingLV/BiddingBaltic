// angular
import { Component, OnInit } from "@angular/core";

// internal
import { AuthService } from "ClientApp/src/app/core/services/auth/auth.service";
import { ButtonsService } from "ClientApp/src/app/core/services/buttons/buttons.service";
import { CustomButtonModel } from "ClientApp/src/app/core/services/buttons/custom-button.model";

@Component({
  selector: "app-home-header",
  templateUrl: "./header.component.html",
  styleUrls: ["./header.component.scss"]
})
export class HomeHeaderComponent implements OnInit {
  buttonConfig: CustomButtonModel;

  constructor(
    private authService: AuthService,
    private buttonsService: ButtonsService
  ) {}

  ngOnInit(): void {
    this.handleSignInButton();
  }

  /** Used to handle sign-in */
  onSignInChange(event: boolean) {
    this.authService.login();
  }

  private handleSignInButton(): void {
    this.buttonConfig = {
      ...this.buttonsService.defaultButtonConfig
    };
  }
}
