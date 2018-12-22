using System;
using System.Collections.Generic;
using ContactOrganizer;
using ContactOrganizer.Data.SqlServer;
using ContactOrganizer.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContractOrganizer.Tests
{    
    [TestClass]
    public class RepositoryTests
    {
        private IContactOrganizerRepository _contactOrganizerSqlRepository;

        public RepositoryTests()
        {
            DbContextOptionsBuilder<ContactOrganizerSqlRepository> optionsBuilder = new DbContextOptionsBuilder<ContactOrganizerSqlRepository>();
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ContactOrganizer", providerOptions => providerOptions.CommandTimeout(60));
            _contactOrganizerSqlRepository = new ContactOrganizerSqlRepository(optionsBuilder.Options);
        }

        [TestInitialize]
        public void Init()
        {
            _contactOrganizerSqlRepository.RemoveAllContacts();
        }

        [TestMethod]
        public void TestCreateNewContact()
        {
            Guid id = Guid.NewGuid();
            Contact newContact = new Contact(id, "Milivoj", "Milani", "+385989059120", new ContactAddress("Tonžino 7b", "Rijeka", "51000", "Croatia"));
            _contactOrganizerSqlRepository.CreateNewContact(newContact);
            Assert.AreEqual(id, _contactOrganizerSqlRepository.GetContactById(id).Id);
        }

        [TestMethod]
        public void TestUpdateContactDetails()
        {
            Guid id = Guid.NewGuid();
            Contact newContact = new Contact(id, "Milivoj", "Milani", "+385989059120", new ContactAddress("Tonžino 7b", "Rijeka", "51000", "Croatia"));
            _contactOrganizerSqlRepository.CreateNewContact(newContact);
            newContact.UpdateDetails("Jovilim", "Inalim", "+385989059120", new ContactAddress("Tonžino 7a", "Rijeka", "51000", "Croatia"));
            Contact contactFromDb = _contactOrganizerSqlRepository.GetContactById(id);
            Assert.AreEqual("Jovilim", contactFromDb.FirstName);
            Assert.AreEqual("Inalim", contactFromDb.LastName);
            Assert.AreEqual("Tonžino 7a", contactFromDb.Address.StreetAndNumber);
        }

        [TestMethod]
        public void TestDeleteContact()
        {
            Guid id = Guid.NewGuid();
            Contact newContact = new Contact(id, "Milivoj", "Milani", "+385989059120", new ContactAddress("Tonžino 7b", "Rijeka", "51000", "Croatia"));
            _contactOrganizerSqlRepository.CreateNewContact(newContact);
            newContact.UpdateDetails("Jovilim", "Inalim", "+385989059120", new ContactAddress("Tonžino 7a", "Rijeka", "51000", "Croatia"));
            Contact contactFromDb = _contactOrganizerSqlRepository.GetContactById(id);
            _contactOrganizerSqlRepository.DeleteContact(contactFromDb.Id);
            Assert.ThrowsException<InvalidOperationException>(() => _contactOrganizerSqlRepository.GetContactById(id));
        }

        [TestMethod]
        public void TestFindFirstName()
        {
            int totalNumber;
            
            List<Contact> result = _contactOrganizerSqlRepository.FindContacts("James", null, null, null, 0, 1, "", out totalNumber);
            Assert.AreEqual(1, totalNumber);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void TestFindLastName()
        {
            int totalNumber;

            List<Contact> result = _contactOrganizerSqlRepository.FindContacts(null, "Keita", null, null, 0, 1, "", out totalNumber);
            Assert.AreEqual(1, totalNumber);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void TestFindAddress()
        {
            int totalNumber;

            List<Contact> result = _contactOrganizerSqlRepository.FindContacts(null, null, null, "Liverpool", 0, 5, "", out totalNumber);
            Assert.AreEqual(11, totalNumber);
            Assert.AreEqual(5, result.Count);
        }
    }
}
