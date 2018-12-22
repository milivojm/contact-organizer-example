using ContactOrganizer.Events;
using ContactOrganizer.Infrastructure;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace ContactOrganizer
{
    /// <summary>
    /// Facade class into this small application.
    /// </summary>
    public class ContactOrganizerService
    {
        private IContactOrganizerRepository _contactOrganizerRepository;
        private readonly INotificationHandler _notificationHandler;

        public ContactOrganizerService(IContactOrganizerRepository contactOrganizerRepository, INotificationHandler notificationHandler)
        {
            _contactOrganizerRepository = contactOrganizerRepository ?? throw new ArgumentNullException(nameof(contactOrganizerRepository));
            _notificationHandler = notificationHandler ?? throw new ArgumentNullException(nameof(notificationHandler));
        }

        /// <summary>
        /// Creates a new contact and saves it to external store.
        /// </summary>
        /// <param name="contactDetails">Contact details value object.</param>
        public void CreateNewContact(IContactDetails contactDetails)
        {
            ContactDetailsValidator contactDetailsValidator = new ContactDetailsValidator();
            ValidationResult validationResult = contactDetailsValidator.Validate(contactDetails);

            if (validationResult.IsValid)
            {
                ContactAddress contactAddress = new ContactAddress(contactDetails.Address);
                Contact newContact = new Contact(Guid.NewGuid(), contactDetails.FirstName, contactDetails.LastName, contactDetails.TelephoneNumber, contactAddress);

                _contactOrganizerRepository.CreateNewContact(newContact);
                _notificationHandler.HandleEvent(new NewContactCreated(newContact));
            }
            else
            {
                _notificationHandler.HandleValidationError(validationResult);
            }
        }

        /// <summary>
        /// Updates contact details.
        /// </summary>
        /// <param name="contactId">Contact unique ID.</param>
        /// <param name="contactDetails">Contact details value object.</param>
        public void UpdateContactDetails(Guid contactId, IContactDetails contactDetails)
        {
            ContactDetailsValidator contactDetailsValidator = new ContactDetailsValidator();
            ValidationResult validationResult = contactDetailsValidator.Validate(contactDetails);

            if (validationResult.IsValid)
            {
                Contact contact = _contactOrganizerRepository.GetContactById(contactId);
                ContactAddress contactAddress = new ContactAddress(contactDetails.Address);

                contact.UpdateDetails(contactDetails.FirstName, contactDetails.LastName, contactDetails.TelephoneNumber, contactAddress);
                _contactOrganizerRepository.UpdateContactDetails(contact);
                _notificationHandler.HandleEvent(new ContactDetailsUpdated(contact));
            }
            else
            {
                _notificationHandler.HandleValidationError(validationResult);
            }
        }

        /// <summary>
        /// Deletes the contact from external data store.
        /// </summary>
        /// <param name="contactId">Contact unique identifier.</param>
        public void DeleteContact(Guid contactId)
        {
            _contactOrganizerRepository.DeleteContact(contactId);
            _notificationHandler.HandleEvent(new ContactDeleted(contactId));
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
