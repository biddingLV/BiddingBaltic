// angular
import { FormGroup, AbstractControl } from "@angular/forms";
import { Injectable } from "@angular/core";

// 3rd lib
import { Subscription } from "rxjs";

@Injectable()
export class FormService {
  unsubscribeFlag = false;

  /**
   * Marks all controls in a form group as touched
   * @param formGroup - The form group to touch
   */
  markFormGroupTouched(formGroup: FormGroup) {
    (<any>Object).values(formGroup.controls).forEach((control: FormGroup) => {
      control.markAsTouched();

      if (control.controls) {
        this.markFormGroupTouched(control);
      }
    });
  }

  /**
   * Gets the right error message for the form control
   * @param fieldName form control
   */
  validationMessages(fieldName: string) {
    const message = this.getRequiredMessage(fieldName);

    // for each unique field there needs to be an error message!
    // todo: kke: combine getRequiredMessage() + messages!
    const messages = {
      required: message + " is required.",
      // this can be used to validate if the field consists of invalid characters,
      // note: kke: it is not really fully tested!
      invalid_characters: (matches: any[]) => {
        let matchedCharacters = matches;

        matchedCharacters = matchedCharacters.reduce(
          (characterString, character, index) => {
            let string = characterString;
            string += character;

            if (matchedCharacters.length !== index + 1) {
              string += ", ";
            }

            return string;
          },
          ""
        );

        return `These characters are not allowed: ${matchedCharacters}`;
      }
    };

    return messages;
  }

  /**
   * This method can be used to validate whole form(all form controls) for example, onSubmit
   * It can also be used to validate specific FormGroup controls for example, onClick
   * note: check_dirty true will only emit errors if the field is touched
   * note: check_dirty false will check all fields independent of
   * being touched or not. Use this as the last check before submitting
   * @param formElement
   * @param formErrors
   * @param checkDirty
   */
  validateForm(
    formElement: FormGroup | AbstractControl,
    formErrors: any,
    checkDirty?: boolean
  ) {
    const form = formElement;

    for (const field in formErrors) {
      if (field) {
        formErrors[field] = "";
        const control = form.get(field);

        // initialize error messages
        const messages = this.validationMessages(field);

        if (control && !control.valid) {
          if (!checkDirty || control.dirty || control.touched) {
            for (const key in control.errors) {
              if (key && key !== "invalid_characters") {
                formErrors[field] = formErrors[field] || messages[key];
              } else {
                formErrors[field] =
                  formErrors[field] || messages[key](control.errors[key]);
              }
            }
          }
        }
      }
    }

    return formErrors;
  }

  /**
   * On close, cancel & backdrop-click dont do anyhing,
   * On submit clean selected values & update information - list/details(if needed)
   * @param result - on Hide result event name
   * @param subscriptions - all subscriptions array
   */
  onModalHide(result: string, subscriptions: Subscription[]): boolean {
    this.unsubscribeFlag = false;

    if (result !== null && result !== "backdrop-click") {
      this.unsubscribeFlag = true;
      this.unsubscribe(subscriptions);
    }

    return this.unsubscribeFlag;
  }

  /**
   * Unsubscribes from subscriptions
   * @param subscriptions - all subscriptions array
   */
  private unsubscribe(subscriptions: Subscription[]): void {
    subscriptions.forEach((subscription: Subscription) => {
      subscription.unsubscribe();
    });

    subscriptions = [];
  }

  /**
   * Gets a user friendly error message for specific form control
   * @param fieldName - form control name
   */
  private getRequiredMessage(fieldName: string): string {
    const messages = {
      // auction
      auctionName: "Nosaukums",
      auctionTopCategory: "Pamatkategorija",
      auctionSubCategory: "Apakškategorija",
      auctionStartingPrice: "Sākumcena",
      auctionStartDate: "Izsoles sākuma datums",
      auctionFormat: "Izsoles veids",
      // auction - vehicle
      vehicleName: "Nosaukums",
      vehicleMake: "Marka",
      vehicleModel: "Modelis",
      vehicleManufacturingYear: "Gads",
      // auction - item category
      itemName: "Nosaukums",
      itemModel: "Modelis",
      itemEvaluation: "Novērtējums",
      // auction - about
      auctionCreator: "Izsoles veidotājs",
      auctionAddress: "Adrese",
      auctionCreatorEmail: "E-pasts",
      auctionCreatorPhone: "Telefons",
      // user
      userFirstName: "First name",
      userLastName: "Last name",
      userEmail: "E-mail",
      userPhone: "Phone"
    };

    return messages[fieldName];
  }
}
