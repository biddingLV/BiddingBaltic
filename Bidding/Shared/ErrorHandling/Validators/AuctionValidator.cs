using Bidding.Shared.ErrorHandling.Errors;
using BiddingAPI.Models.DatabaseModels.Bidding;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Shared.ErrorHandling.Validators
{
    public class AuctionValidator : AbstractValidator<Auction>
    {
        public AuctionValidator()
        {
            // todo: kke: improve this!
            //RuleFor(org => org.OrganizationName)
            //    .NotNull().WithMessage(EnumHelper.GetDescriptionFromValue(AuctionErrorMessages.MissingOrganizationName))
            //    .Length(0, 100).WithMessage(EnumHelper.GetDescriptionFromValue(AuctionErrorMessages.OrganizationsNameTooLong));

            //RuleFor(org => org.SupplierId)
            //    .NotNull().WithMessage(EnumHelper.GetDescriptionFromValue(OrganizationErrorMessages.MissingSupplier));

            //RuleFor(org => org.OrganizationTypeId)
            //    .NotNull().WithMessage(EnumHelper.GetDescriptionFromValue(OrganizationErrorMessages.MissingOrganizationType));

            //RuleFor(org => org.Address1)
            //    .NotNull().WithMessage(EnumHelper.GetDescriptionFromValue(OrganizationErrorMessages.MissingStreetAddress))
            //    .MaximumLength(100).WithMessage(EnumHelper.GetDescriptionFromValue(OrganizationErrorMessages.StreetAddressTooLong)); // in DB this can be 1000!

            //RuleFor(org => org.City)
            //    .NotNull().WithMessage(EnumHelper.GetDescriptionFromValue(OrganizationErrorMessages.MissingCity))
            //    .MaximumLength(100).WithMessage(EnumHelper.GetDescriptionFromValue(OrganizationErrorMessages.CityTooLong));

            //RuleFor(org => org.PostalCode)
            //    .NotNull().WithMessage(EnumHelper.GetDescriptionFromValue(OrganizationErrorMessages.MissingPostalCode))
            //    .MaximumLength(50).WithMessage(EnumHelper.GetDescriptionFromValue(OrganizationErrorMessages.PostalCodeTooLong));

            //RuleFor(org => org.Country)
            //    .NotNull().WithMessage(EnumHelper.GetDescriptionFromValue(OrganizationErrorMessages.MissingCountry))
            //    .MaximumLength(50).WithMessage(EnumHelper.GetDescriptionFromValue(OrganizationErrorMessages.CountryTooLong));

            //RuleFor(org => org.Email)
            //    .NotNull().WithMessage(EnumHelper.GetDescriptionFromValue(OrganizationErrorMessages.MissingOrganizationsEmail))
            //    .MaximumLength(50).WithMessage(EnumHelper.GetDescriptionFromValue(OrganizationErrorMessages.OrganizationsEmailTooLong));
        }
    }
}
