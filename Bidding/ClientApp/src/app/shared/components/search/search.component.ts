import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {
  @Input() placeholder;
  @Output() search = new EventEmitter<string>();
  searchText: string;

  constructor() { }

  ngOnInit() { }

  onSearch(text: string) {
    this.search.emit(text);
  }
}
