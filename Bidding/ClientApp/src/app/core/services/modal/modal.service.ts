// angular
import { Injectable } from "@angular/core";

// 3rd lib
import { map, take } from "rxjs/operators";
import { pipe } from "rxjs";

@Injectable()
export class ModalService {
  defaultModalOptions = {
    class: "modal-md",
    ignoreBackdropClick: true,
    initialState: {}
  };

  toModalResult = () =>
    pipe(
      map(result => {
        if (result !== null && result !== "backdrop-click") {
          return { success: true, result: result };
        }
        return { success: false, result: result };
      }),
      take(1)
    );
}
