// angular
import { Component, OnInit } from '@angular/core';

// internal
import { HomepageCardModel } from '../../models/homepage-card.model';

@Component({
  selector: 'app-home-cards',
  templateUrl: './cards.component.html',
  styleUrls: ['./cards.component.scss'],
})
export class HomeCardsComponent implements OnInit {
  cards: HomepageCardModel[];

  constructor() {}

  ngOnInit(): void {
    this.cards = [
      {
        cardTitle: 'Pārdot garāžu izsolē',
        linkDescription: 'Pieteikt mantu garāžu izsolei',
        link: 'https://forms.gle/L1uhCpqfM3eFVHTY9',
      },
      {
        cardTitle: 'Pirkt garāžu izsolē',
        linkDescription: 'Reģistrēties kā pircējam',
        link: 'https://forms.gle/poiGEM2RSAFYAULM7',
      },
    ];
  }
}
