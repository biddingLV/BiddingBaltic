// angular
import { Component, OnInit } from "@angular/core";

// internal
import { HomepageCardModel } from "../../models/homepage-card.model";

@Component({
  selector: "app-home-cards",
  templateUrl: "./cards.component.html",
  styleUrls: ["./cards.component.scss"],
})
export class HomeCardsComponent implements OnInit {
  cards: HomepageCardModel[];

  constructor() {}

  ngOnInit(): void {
    this.cards = [
      {
        cardTitle: "Pārdot garāžu izsolē",
        linkDescription: "Pieteikt mantu garāžu izsolei",
      },
      {
        cardTitle: "Pirkt garāžu izsolē",
        linkDescription: "Reģistrēties kā pircējam",
      },
    ];
  }
}
