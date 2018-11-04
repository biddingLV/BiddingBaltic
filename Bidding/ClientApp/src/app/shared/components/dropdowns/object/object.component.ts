import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
// import { IDropdownConfig, IDropdownOption, IDropdownItem } from '../../../models/dropdown.model';

@Component({
  selector: 'app-object-dropdown',
  templateUrl: './object.component.html'
})
export class ObjectDropdownComponent {
  // @Input() public config: IDropdownConfig;
  // tslint:disable-next-line:no-any
  @Output() public idChange = new EventEmitter<any>();
  // tslint:disable-next-line:no-any
  public onIdChange(id) {
    this.idChange.emit(id);
  }

}
