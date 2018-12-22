using ContactOrganizer.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace ContactOrganizer.Data.SqlServer
{
    public class ContactOrganizerSqlRepository : DbContext, IContactOrganizerRepository
    {
        public ContactOrganizerSqlRepository(DbContextOptions<ContactOrganizerSqlRepository> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Contact
            modelBuilder.Entity<Contact>().Property(c => c.FirstName).HasField("_firstName").HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Contact>().Property(c => c.LastName).HasField("_lastName").HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Contact>().Property(c => c.TelephoneNumber).HasField("_telephoneNumber").HasMaxLength(15).IsRequired();
            modelBuilder.Entity<Contact>().OwnsOne<ContactAddress>("_contactAddress");
            modelBuilder.Entity<Contact>().Property(c => c.FullAddress).HasColumnName("FullAddress").HasMaxLength(200);
            modelBuilder.Entity<Contact>().HasIndex(c => new { c.FirstName, c.LastName }).IsUnique();
            #endregion

            #region ContactAddress
            modelBuilder.Entity<ContactAddress>().Property("StreetAndNumber").HasColumnName("StreetNumber").HasMaxLength(80).IsRequired();
            modelBuilder.Entity<ContactAddress>().Property("City").HasColumnName("City").HasMaxLength(40).IsRequired();
            modelBuilder.Entity<ContactAddress>().Property("PostalCode").HasColumnName("PostalCode").HasMaxLength(20);
            modelBuilder.Entity<ContactAddress>().Property("Country").HasColumnName("Country").HasMaxLength(50);
            
            #endregion
        }

        public void CreateNewContact(Contact newContact)
        {
            Contacts.Add(newContact);
            SaveChanges();
        }

        public void DeleteContact(Guid contactId)
        {
            Contact contactToDelete = Contacts.Find(contactId);
            Contacts.Remove(contactToDelete);
            SaveChanges();
        }

        public List<Contact> FindContacts(string firstName, string lastName, string telephoneNumber, string address, int takeFrom, int count, string sortExpression, out int totalNumber)
        {
            string orderByColumn = "FirstName";

            Regex sortRegex = new Regex(@"^(?<sortColumn>FirstName|LastName|TelephoneNumber|FullAddress)$");
            Match match = sortRegex.Match(sortExpression);

            if (match.Success)
            {
                orderByColumn = match.Groups["sortColumn"].Value;                
            }

            var query = Contacts.AsQueryable();

            if (!string.IsNullOrEmpty(firstName))
                query = query.Where(c => c.FirstName.StartsWith(firstName));

            if (!string.IsNullOrEmpty(lastName))
                query = query.Where(c => c.LastName.StartsWith(lastName));

            if (!string.IsNullOrEmpty(telephoneNumber))
                query = query.Where(c => c.TelephoneNumber.StartsWith(telephoneNumber));

            if (!string.IsNullOrEmpty(address))
                query = query.Where(c => c.FullAddress.Contains(address));

            totalNumber = query.Count();
            
            switch (orderByColumn)
            {
                case "FirstName":
                    query = query.OrderBy(c => c.FirstName);
                    break;
                case "LastName":
                    query = query.OrderBy(c => c.LastName);
                    break;
                case "TelephoneNumber":
                    query = query.OrderBy(c => c.TelephoneNumber);
                    break;
                case "FullAddress":
                    query = query.OrderBy(c => c.FullAddress);
                    break;
            }

            return query.Skip(takeFrom).Take(count).ToList();
        }

        public Contact GetContactById(Guid contactId)
        {
            return Contacts.First(c => c.Id == contactId);
        }

        public void UpdateContactDetails(Contact contact)
        {
            Entry(contact).State = EntityState.Modified;
            SaveChanges();
        }

        public void RemoveAllContacts()
        {
            Database.ExecuteSqlCommand("TRUNCATE TABLE Contacts");
        }
    }
}
