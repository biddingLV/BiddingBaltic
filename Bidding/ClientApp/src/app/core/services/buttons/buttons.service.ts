// angular
import { Injectable } from "@angular/core";

// internal
import { CustomButtonModel } from "./custom-button.model";

@Injectable()
export class ButtonsService {
  defaultButtonConfig: CustomButtonModel = {
    styles: {
      cursor: "pointer",
      fontSize: "14px"
    },
    class:
      "btn-secondary btn-lg rounded-pill bg-transparent border-white text-white font-weight-bold"
  };
}
