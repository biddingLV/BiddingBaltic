using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Bidding.Validation.ReCaptcha
{
  public abstract class GoogleReCaptchaModelBase
  {
    [Required]
    [GoogleReCaptchaValidation]
    [BindProperty(Name = "g-recaptcha-response")]
    public String GoogleReCaptchaResponse { get; set; }
  }
}
