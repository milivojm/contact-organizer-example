using FluentValidation;
using FluentValidation.Validators;

namespace ContactOrganizer.Infrastructure
{
    public class ContactAddressValidator : AbstractValidator<IContactAddress>
    {
        public ContactAddressValidator()
        {
            RuleFor(address => address.StreetAndNumber).NotEmpty().MaximumLength(80);
            RuleFor(address => address.City).NotEmpty().MaximumLength(40);
            RuleFor(address => address.PostalCode).MaximumLength(20);
            RuleFor(address => address.Country).MaximumLength(50);
        }
    }
}