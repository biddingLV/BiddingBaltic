// angular
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { Component } from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
// 3rd party
import {DatetimePopupModule} from 'ngx-bootstrap-datetime-popup';

@Component({
    selector: 'datetimepopup',
    template: '.\datetimepopup.component.html'
})
export class DatetimePopupComponent {

    showPicker : boolean;
    myDate: Date = new Date();
    showDate = true;
    showTime = true;
    closeButton = { show: true, label: 'Aizvērt', cssClass: 'btn btn-sm btn-primary' };

    onTogglePicker() {
        if (this.showPicker === false) {
            this.showPicker = true;
        }
    }

    onValueChange(selectedDate: Date) {
        this.myDate = selectedDate;
    }

    isValid(): boolean {
      // this function is only here to stop the datepipe from erroring if someone types in value
        return this.myDate && (typeof this.myDate !== 'string') && !isNaN(this.myDate.getTime());
    }
}

export class AppModule {
}

platformBrowserDynamic().bootstrapModule(AppModule);