using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactOrganizer.Infrastructure
{
    public class ContactDetailsValidator : AbstractValidator<IContactDetails>
    {
        public ContactDetailsValidator()
        {
            RuleFor(contact => contact.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(contact => contact.LastName).NotEmpty().MaximumLength(50);
            RuleFor(contact => contact.TelephoneNumber).NotEmpty();
            RuleFor(contact => contact.TelephoneNumber).Matches(Contact.TelephoneNumberRegex);
            RuleFor(contact => contact.Address).SetValidator(new ContactAddressValidator());
        }
    }
}
