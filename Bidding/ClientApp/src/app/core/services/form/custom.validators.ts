// angular
import { FormControl, Validators } from "@angular/forms";

/*
 *
 * If you change these regular expressions, also update API-side regular expressions!
 *
 */

/**
 * We get a more practical implementation of RFC 5322 if we omit IP addresses, domain-specific addresses, the syntax using double quotes and square brackets.
 * It will still match 99.99% of all email addresses in actual use today. Also allow capital letters!
 * Source - regular-expressions.info website
 */
const emailRegExp = /[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?/;

/**
 * Example - 694,21 || 694.21 both valid. [.,] chars are optional
 */
const decimalRegExp = /^\d*[\.,]?\d*$/;

/**
 * Valid years from 1000 to 2999
 */
const yearRegExp = /^[12][0-9]{3}$/;

export class CustomValidators extends Validators {
  static validatePrice(control: FormControl) {
    return CustomValidators.handleFieldChange(control, decimalRegExp, {
      invalidPrice: true
    });
  }

  static validateMeasurementValue(control: FormControl) {
    return CustomValidators.handleFieldChange(control, decimalRegExp, {
      measurementValue: true
    });
  }

  static validateOnlyYear(control: FormControl) {
    return CustomValidators.handleFieldChange(control, yearRegExp, {
      invalidYear: true
    });
  }

  private static handleFieldChange(
    control: FormControl,
    regExp: RegExp,
    formFieldErrorObject: any
  ) {
    if (control.pristine) {
      return null;
    }

    control.markAsTouched();

    if (control.value && control.value.length > 0) {
      if (regExp.test(control.value)) {
        return null;
      } else {
        return formFieldErrorObject;
      }
    } else {
      return null;
    }
  }
}
