import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search.component.html',
  styleUrls: []
})
export class SearchComponent implements OnInit {
  @Input() placeholder: string;
  @Output() search = new EventEmitter<string>();
  searchText: string;

  constructor() { }

  ngOnInit() { }

  onSearch(text: string) {
    this.search.emit(text);
  }
}
