// angular
import { Component, OnInit } from "@angular/core";

@Component({
  selector: "app-home-cards",
  templateUrl: "./cards.component.html",
  styleUrls: ["./cards.component.scss"]
})
export class HomeCardsComponent implements OnInit {
  cards = [
    {
      description: "Transportlīdzekļu izsoles",
      imagePath: "../../../../../assets/images/delivery-truck.svg",
      filterUrl: "transports"
    },
    {
      description: "Nekustamo īpašumu izsoles",
      imagePath: "../../../../../assets/images/apartments.svg",
      filterUrl: "ipasumi"
    },
    {
      description: "Mantu izsoles",
      imagePath: "../../../../../assets/images/shelf.svg",
      filterUrl: "mantas"
    }
  ];

  constructor() {}

  ngOnInit(): void {}
}
