// angular
import { Component, OnInit } from "@angular/core";

@Component({
  selector: "app-home-cards",
  templateUrl: "./cards.component.html",
  styleUrls: ["./cards.component.scss"]
})
export class HomeCardsComponent implements OnInit {
  public cards = [
    {
      description: "Transportlīdzekļu izsoles",
      imagePath: "../../../../../assets/images/motorcycle.png"
    },
    {
      description: "Nekustamo īpašumu izsoles",
      imagePath: "../../../../../assets/images/motorcycle.png"
    },
    {
      description: "Mantu izsoles",
      imagePath: "../../../../../assets/images/motorcycle.png"
    }
  ];

  constructor() {}

  ngOnInit(): void {}
}
