// angular
import { Component, OnInit, Output, EventEmitter, Input } from "@angular/core";

@Component({
  selector: "app-search-bar",
  templateUrl: "./search.component.html"
})
export class SearchComponent implements OnInit {
  @Output() searchChange = new EventEmitter<string>();

  searchText: string;

  constructor() {}

  ngOnInit(): void {}

  onSearch(text: string): void {
    this.searchChange.emit(text);
  }
}
