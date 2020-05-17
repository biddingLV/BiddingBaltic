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
        cardTitle: "Piedalīties Garāžu izsolē",
        linkDescription: "Reģistrēties kā dalībniekam",
      },
      {
        cardTitle: "Vēlos pārdot Garāžu izsolē",
        linkDescription: "Pieteikt mantu “Garāžu izsolei”",
      },
    ];
  }
}
