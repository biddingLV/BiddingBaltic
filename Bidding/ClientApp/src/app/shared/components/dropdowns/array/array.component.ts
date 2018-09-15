import { Component, Input, Output, EventEmitter } from '@angular/core';
// import { IDropdownConfig } from '../../../models/dropdown.model';

@Component({
  selector: 'array-dropdown',
  templateUrl: './array.component.html'
})
export class ArrayDropdownComponent {
  // @Input() public config: IDropdownConfig;
  // tslint:disable-next-line:no-any
  @Output() public itemChange = new EventEmitter<any>();

  public onItemChange(item) {
    this.itemChange.emit(item);
  }

}
