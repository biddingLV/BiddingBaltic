import { FormControl, Validators } from '@angular/forms';

const validOrgName = new RegExp('/^[a-zA-ZÀ-ú0-9_ \\/,.\\.]*$\/');
// todo: kke: invalid reg exp
const validStreet = new RegExp(''); // ^[a-zA-Z0-9À-ú\\x7f-\xff_ \/,.\\.\\-\\:\\_\\!\\?\\(\\)\\#\\&\\]*$
// todo: kke: invalid reg exp
const validCity = new RegExp(''); // [a-zA-Z0-9À-ú\\x7f-\xff_ \/,.\\.\\-\\:\\_\\!\\?\\(\\)\\#\\&\\]*$
const validPostalCode = new RegExp('^[a-zA-Z0-9\\x7f-\xff_ \/,.\\.\\&\\+\\-]*$');
const validEmail = new RegExp('^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,63}$');
// kke: old up user add email - ng-pattern='/^(([^<>()\[\]\\.,;:\s@"]+(\.[^><>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/'
const validPhone = new RegExp('/\\+? ?(\\d+|\\(\\d+\\))+(( ?|\\-?)\\d+|( ?|\\-?)\\(\\d+\\))*/');
// kke: old UP user add phone - ng-pattern="/^\+? ?(\d+|\(\d+\))+(( ?|\-?)\d+|( ?|\-?)\(\d+\))*$/"
const validWebsite = new RegExp('(http(s)?:\\\\.)?(www\\.)?[-a-zA-Z0-9ÆØÅæøå@:%._\\+~#=]{2,256}\\.[a-z]{2,63}\\b([-a-zA-Z0-9@:%_\\+.~#?&=]*)');
const validFirstName = new RegExp('^[a-zA-Z0-9_ -\.\u00C0-\u00D6\u00D8-\u00F6\u00F8-\u00FF]*$');
const validLastName = new RegExp('^[a-zA-Z0-9_ -\.\u00C0-\u00D6\u00D8-\u00F6\u00F8-\u00FF]*$');

export class CustomValidators extends Validators {
  static validateOrgName(control: FormControl) {
    if (control.value && control.value.length > 0) {
      return !validOrgName.test(control.value) ? { organization: false } : null;
    } else {
      return null;
    }
  }

  static validateStreet(control: FormControl) {
    if (control.value && control.value.length > 0) {
      return !validStreet.test(control.value) ? { street: false } : null;
    } else {
      return null;
    }
  }

  static validateCity(control: FormControl) {
    if (control.value && control.value.length > 0) {
      return !validCity.test(control.value) ? { city: false } : null;
    } else {
      return null;
    }
  }

  static validatePostalCode(control: FormControl) {
    if (control.value && control.value.length > 0) {
      return !validPostalCode.test(control.value) ? { postalCode: false } : null;
    } else {
      return null;
    }
  }

  static validateSignInEmail(control: FormControl) {
    if (control.value && control.value.length > 0) {
      return !validEmail.test(control.value) ? { signInEmail: false } : null;
    } else {
      return null;
    }
  }

  static validatePhone(control: FormControl) {
    if (control.value && control.value.length > 0) {
      return !validPhone.test(control.value) ? { phone: false } : null;
    } else {
      return null;
    }
  }

  static validateWebsite(control: FormControl) {
    if (control.value && control.value.length > 0) {
      return !validWebsite.test(control.value) ? { website: false } : null;
    } else {
      return null;
    }
  }
}
