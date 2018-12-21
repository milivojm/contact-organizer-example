using ContactOrganizer.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ContactOrganizer.Data.SqlServer
{
    public class ContactOrganizerSqlRepository : DbContext, IContactOrganizerRepository
    {
        private bool _aspNetCore;

        public ContactOrganizerSqlRepository()
        {
            _aspNetCore = false;
        }

        public ContactOrganizerSqlRepository(DbContextOptions<ContactOrganizerSqlRepository> options) : base(options)
        {
            _aspNetCore = true;
        }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!_aspNetCore)
                optionsBuilder
                    .UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ContactOrganizer", providerOptions => providerOptions.CommandTimeout(60));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Contact
            modelBuilder.Entity<Contact>().Property(c => c.FirstName).HasField("_firstName").HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Contact>().Property(c => c.LastName).HasField("_lastName").HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Contact>().Property(c => c.TelephoneNumber).HasField("_telephoneNumber").HasMaxLength(15).IsRequired();
            modelBuilder.Entity<Contact>().OwnsOne<ContactAddress>("_contactAddress");
            modelBuilder.Entity<Contact>().HasIndex(c => new { c.FirstName, c.LastName }).IsUnique();
            #endregion

            #region ContactAddress
            modelBuilder.Entity<ContactAddress>().Property("StreetAndNumber").HasColumnName("StreetNumber").HasMaxLength(80).IsRequired();
            modelBuilder.Entity<ContactAddress>().Property("City").HasColumnName("City").HasMaxLength(40).IsRequired();
            modelBuilder.Entity<ContactAddress>().Property("PostalCode").HasColumnName("PostalCode").HasMaxLength(20);
            modelBuilder.Entity<ContactAddress>().Property("Country").HasColumnName("Country").HasMaxLength(50);
            modelBuilder.Entity<ContactAddress>().Property("FullAddress").HasField("_fullAddress").HasColumnName("FullAddress").HasMaxLength(200);
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

        public List<Contact> FindContacts(string firstName, string lastName, string telephoneNumber, string address, int takeFrom, int count, string orderByField, out int totalNumber)
        {
            SqlParameter numberOfRowsParameter = new SqlParameter
            {
                ParameterName = "totalNumber",
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Output
            };

            string sql = $"exec FindContacts @firstName, @lastName, @telephoneNumber, @address, @from, @count, @orderByField, @totalNumber OUT";
            List<Contact> result = Contacts.FromSql(sql, firstName, lastName, telephoneNumber, address, takeFrom, count, orderByField, numberOfRowsParameter).ToList();
            totalNumber = (int)numberOfRowsParameter.Value;
            return result;
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
