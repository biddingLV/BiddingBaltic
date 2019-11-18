import { Component, OnInit } from "@angular/core";
import { AuthService } from "ClientApp/src/app/core/services/auth/auth.service";

@Component({
  selector: "app-home-header",
  templateUrl: "./header.component.html",
  styleUrls: ["./header.component.scss"]
})
export class HomeHeaderComponent implements OnInit {
  constructor(private authService: AuthService) {}

  ngOnInit(): void {}

  /** Used to handle sign-in */
  onSignInChange(event: boolean) {
    this.authService.login();
  }
}
