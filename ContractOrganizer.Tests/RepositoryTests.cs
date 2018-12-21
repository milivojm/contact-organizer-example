using System;
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

        [TestCleanup]
        public void Cleanup()
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
    }
}
