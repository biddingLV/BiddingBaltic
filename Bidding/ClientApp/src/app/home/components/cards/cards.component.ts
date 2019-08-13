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
      imagePath: "../../../../../assets/images/delivery-truck.svg"
    },
    {
      description: "Nekustamo īpašumu izsoles",
      imagePath: "../../../../../assets/images/delivery-truck.svg"
    },
    {
      description: "Mantu izsoles",
      imagePath: "../../../../../assets/images/delivery-truck.svg"
    }
  ];

  constructor() {}

  ngOnInit(): void {}
}
