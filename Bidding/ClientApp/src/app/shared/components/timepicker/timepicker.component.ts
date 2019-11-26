// angular
import { Component, Input, Output, EventEmitter } from "@angular/core";

// 3rd lib
import { TimepickerConfig } from "ngx-bootstrap/timepicker";

export function getTimepickerConfig(): TimepickerConfig {
  return Object.assign(new TimepickerConfig(), {
    hourStep: 1,
    minuteStep: 10,
    showMeridian: false,
    readonlyInput: false,
    mousewheel: true,
    showMinutes: true,
    showSeconds: false,
    showSpinners: true
  });
}

@Component({
  selector: "app-timepicker",
  templateUrl: "./timepicker.component.html",
  providers: [{ provide: TimepickerConfig, useFactory: getTimepickerConfig }]
})
export class TimepickerComponent {
  @Input() chosenTime: Date;
  @Output() timeChange = new EventEmitter<Date>();

  onTimeChange(event) {
    this.timeChange.emit(this.chosenTime);
  }
}
