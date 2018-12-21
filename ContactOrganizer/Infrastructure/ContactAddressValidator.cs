using FluentValidation;
using FluentValidation.Validators;

namespace ContactOrganizer.Infrastructure
{
    public class ContactAddressValidator : AbstractValidator<IContactAddress>
    {
        public ContactAddressValidator()
        {
            RuleFor(address => address.StreetAndNumber).NotEmpty();
            RuleFor(address => address.Country).NotEmpty();
        }
    }
}