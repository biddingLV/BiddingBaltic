// angular
import { FormGroup, AbstractControl } from "@angular/forms";
import { Injectable } from "@angular/core";

// 3rd lib
import { BsDatepickerConfig } from "ngx-bootstrap/datepicker/bs-datepicker.config";

@Injectable()
export class FormService {
  /**
   * Ngx-bootstrap datepicker global config
   */
  bsConfig: Partial<BsDatepickerConfig> = {
    containerClass: "theme-green",
    dateInputFormat: "YYYY-MM-DD",
    showWeekNumbers: true,
    isAnimated: true,
    adaptivePosition: true
  };

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
    const fieldMessage = this.getRequiredMessage(fieldName);

    const messages = {
      required: fieldMessage + " is required.",
      invalidPrice: fieldMessage + " has invalid pattern.",
      invalidYear: fieldMessage + " has invalid pattern."
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
              formErrors[field] = formErrors[field] || messages[key];
            }
          }
        }
      }
    }

    return formErrors;
  }

  /**
   * Adds files to form data object, used for file upload.
   * @param formData Form data object
   * @param files List with files, can be images, pdfs and so on.
   */
  addFilesToFormData(formData: FormData, files: File[]) {
    for (let i = 0; i < files.length; i++) {
      let item = files[i];

      formData.append(item.name, item);
    }
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
      userFirstName: "Vārds",
      userLastName: "Uzvārds",
      userEmail: "E-mail",
      userPhone: "Telefons"
    };

    return messages[fieldName];
  }
}
