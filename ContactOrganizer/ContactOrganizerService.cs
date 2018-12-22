using ContactOrganizer.Infrastructure;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using FluentValidation;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace ContactOrganizer
{
    /// <summary>
    /// Facade class into this small application.
    /// </summary>
    /// <remarks>This small app uses Hexagonal architecture (Ports and Adapters) principle. For more info see <see cref="https://blog.ndepend.com/hexagonal-architecture/"/></remarks>
    public class ContactOrganizerService
    {
        private IContactOrganizerRepository _contactOrganizerRepository;

        public ContactOrganizerService(IContactOrganizerRepository contactOrganizerRepository)
        {
            _contactOrganizerRepository = contactOrganizerRepository ?? throw new ArgumentNullException(nameof(contactOrganizerRepository));
        }

        /// <summary>
        /// Creates a new contact and saves it to external store.
        /// </summary>
        /// <param name="contactDetails">Contact details value object.</param>
        /// <returns>New Contact created.</returns>
        public Contact CreateNewContact(IContactDetails contactDetails)
        {
            ContactDetailsValidator contactDetailsValidator = new ContactDetailsValidator();
            contactDetailsValidator.ValidateAndThrow(contactDetails);

            ContactAddress contactAddress = new ContactAddress(contactDetails.Address);
            Contact newContact = new Contact(Guid.NewGuid(), contactDetails.FirstName, contactDetails.LastName, contactDetails.TelephoneNumber, contactAddress);
            _contactOrganizerRepository.CreateNewContact(newContact);
            return newContact;
        }

        /// <summary>
        /// Returns a contact by Id.
        /// </summary>
        /// <param name="id">Contact Id.</param>
        /// <returns>Contact with the Id requested.</returns>
        public Contact GetContactById(Guid id)
        {
            return _contactOrganizerRepository.GetContactById(id);
        }

        /// <summary>
        /// Updates contact details.
        /// </summary>
        /// <param name="contactId">Contact unique ID.</param>
        /// <param name="contactDetails">Contact details value object.</param>
        public Contact UpdateContactDetails(Guid contactId, IContactDetails contactDetails)
        {
            ContactDetailsValidator contactDetailsValidator = new ContactDetailsValidator();
            contactDetailsValidator.ValidateAndThrow(contactDetails);

            Contact contact = _contactOrganizerRepository.GetContactById(contactId);
            ContactAddress contactAddress = new ContactAddress(contactDetails.Address);
            contact.UpdateDetails(contactDetails.FirstName, contactDetails.LastName, contactDetails.TelephoneNumber, contactAddress);
            _contactOrganizerRepository.UpdateContactDetails(contact);

            return contact;
        }

        /// <summary>
        /// Deletes the contact from external data store.
        /// </summary>
        /// <param name="contactId">Contact unique identifier.</param>
        public void DeleteContact(Guid contactId)
        {
            _contactOrganizerRepository.DeleteContact(contactId);
        }

        /// <summary>
        /// Returns a list of contacts that satisfy the search criteria. Includes results that contain the criteria string. Case insensitive.
        /// </summary>
        /// <param name="firstName">First name to search for.</param>
        /// <param name="lastName">Last name to search for.</param>
        /// <param name="telephoneNumber">Telephone number to search for.</param>
        /// <param name="address">Address to search for.</param>
        /// <param name="takeFrom">Contact result row number to start list from. Useful in paginations. </param>
        /// <param name="count">Number of contacts to retreive.</param>
        /// <param name="sortExpression">Contact property to order results by.</param>
        /// <param name="totalNumber">Returns total number of contacts in the organizer.</param>
        /// <returns></returns>
        public List<Contact> FindContacts(string firstName, string lastName, string telephoneNumber, string address, int takeFrom, int count, string sortExpression, out int totalNumber)
        {
            return _contactOrganizerRepository.FindContacts(firstName, lastName, telephoneNumber, address, takeFrom, count, sortExpression, out totalNumber);
        }
    }
}
