// angular
import { Injectable } from '@angular/core';


@Injectable()
export class ModalService {
  defaultModalOptions = {
    class: '',
    ignoreBackdropClick: true,
    initialState: {}
  };
}
