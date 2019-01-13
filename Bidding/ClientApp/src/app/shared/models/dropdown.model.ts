export interface IDropdownConfig {
  Title: string;
  CustomOption: IDropdownOption;
  Options: any[];
  Item;
}

export interface IDropdownItem {
  Id;
  Name: string;
}

export interface IDropdownOption {
  Disabled: boolean;
  Name: string;
  Value;
}
