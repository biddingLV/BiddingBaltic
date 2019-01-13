import { Injectable } from '@angular/core';
import { IDropdownOption, IDropdownConfig, IDropdownItem } from '../../models/dropdown.model';


@Injectable({
  providedIn: 'root'
})
export class DropdownService {
  public ListDropdown(filter: any[], item, title: string): IDropdownConfig {
    const config: IDropdownConfig = {
      CustomOption: this.GetListOption(),
      Options: filter,
      Item: item,
      Title: title
    };

    return config;
  }

  public CustomDropdown(option: IDropdownOption, filter: IDropdownItem[], item, title: string): IDropdownConfig {
    const config: IDropdownConfig = {
      CustomOption: option,
      Options: filter,
      Item: item,
      Title: title
    };

    return config;
  }

  public FeatureDropdown(filter: IDropdownItem[], item, title: string): IDropdownConfig {
    const config: IDropdownConfig = {
      CustomOption: this.GetFeatureOption(),
      Options: filter,
      Item: item,
      Title: title
    };

    return config;
  }

  public FeatureListDropdown(filter: any[], item, title: string): IDropdownConfig {
    const config: IDropdownConfig = {
      CustomOption: this.GetFeatureOption(),
      Options: filter,
      Item: item,
      Title: title
    };

    return config;
  }

  public EditListDropdown(filter: any[], item, title: string): IDropdownConfig {
    const config: IDropdownConfig = {
      CustomOption: null,
      Options: filter,
      Item: item,
      Title: title
    };

    return config;
  }

  public GetOption(disabled: boolean, name: string, value): IDropdownOption {
    const option: IDropdownOption = {
      Disabled: disabled,
      Name: name,
      Value: value
    };

    return option;
  }

  private GetListOption(): IDropdownOption {
    return this.GetOption(false, 'All', undefined);
  }

  private GetFeatureOption(): IDropdownOption {
    return this.GetOption(false, 'Unlimited', -1);
  }
}
