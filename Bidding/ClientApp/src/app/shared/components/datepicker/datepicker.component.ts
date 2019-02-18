// todo: kke: example
import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';

@Component({
  selector: 'app-date-picker',
  templateUrl: './datepicker.component.html'
})
export class DatepickerComponent implements OnInit {
  @Input() date: Date;
  @Output() dateChange = new EventEmitter<any>();

  bsConfig: Partial<BsDatepickerConfig>;

  constructor() {
    this.bsConfig = {
      containerClass: 'theme-orange',
      dateInputFormat: 'YYYY-MM-DD',
      showWeekNumbers: true
    };
  }

  ngOnInit() { }

  onDateChange(event) {
    this.dateChange.emit(event);
  }
}
