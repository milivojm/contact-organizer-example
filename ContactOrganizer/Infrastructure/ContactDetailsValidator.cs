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
            RuleFor(contact => contact.StreetAndNumber).NotEmpty().MaximumLength(80);
            RuleFor(contact => contact.City).NotEmpty().MaximumLength(40);
            RuleFor(contact => contact.PostalCode).MaximumLength(20);
            RuleFor(contact => contact.Country).MaximumLength(50);
        }
    }
}
