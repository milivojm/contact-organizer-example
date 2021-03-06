﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ContactOrganizer.Infrastructure
{
    /// <summary>
    /// Interface (Port!) to any data repository. See repository pattern for more details.
    /// </summary>
    public interface IContactOrganizerRepository
    {
        void CreateNewContact(Contact newContact);
        Contact GetContactById(Guid contactId);
        void UpdateContactDetails(Contact contact);
        void DeleteContact(Guid contactId);
        List<Contact> FindContacts(string firstName, string lastName, string telephoneNumber, string address, int takeFrom, int count, string sortExpression, out int totalNumber);
        void RemoveAllContacts();
    }
}