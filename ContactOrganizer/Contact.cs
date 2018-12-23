using ContactOrganizer.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ContactOrganizer
{
    public class Contact
    {
        public static readonly string TelephoneNumberRegex = @"^\+\d{10,12}$";
        private string _firstName;
        private string _lastName;
        private string _telephoneNumber;
        private IContactAddress _contactAddress;

        // EF Core limitations
        private Contact() { }

        public Contact(Guid id, string firstName, string lastName, string telephoneNumber, IContactAddress contactAddress)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            TelephoneNumber = telephoneNumber;
            _contactAddress = contactAddress;
            FullAddress = contactAddress.GetFullAddress();
        }

        /// <summary>
        /// Unique contact identifier.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Contact first name.
        /// </summary>
        public string FirstName
        {
            get => _firstName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("First name cannot be empty.");

                _firstName = value;
            }
        }

        /// <summary>
        /// Contact last name.
        /// </summary>
        public string LastName
        {
            get => _lastName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Last name cannot be empty.");

                _lastName = value;
            }
        }

        /// <summary>
        /// Contact telephone number.
        /// </summary>
        public string TelephoneNumber
        {
            get => _telephoneNumber;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Telephone number cannot be empty.");

                Regex telephoneRegex = new Regex(TelephoneNumberRegex);

                if (!telephoneRegex.IsMatch(value))
                    throw new FormatException("Invalid telephone number format.");

                _telephoneNumber = value;
            }
        }

        /// <summary>
        /// Returns the full address value object.
        /// </summary>
        public IContactAddress Address
        {
            get => _contactAddress;
        }

        /// <summary>
        /// Additional field to speed up searching contacts over address.
        /// </summary>
        public string FullAddress
        {
            get; private set;
        }

        /// <summary>
        /// Updates contact details.
        /// </summary>
        /// <param name="firstName">Contact first name.</param>
        /// <param name="lastName">Contact last name.</param>
        /// <param name="telephoneNumber">Contact telephone number.</param>
        /// <param name="contactAddress">Contact address.</param>
        public void UpdateDetails(string firstName, string lastName, string telephoneNumber, IContactAddress contactAddress)
        {
            FirstName = firstName;
            LastName = lastName;
            TelephoneNumber = telephoneNumber;
            _contactAddress = contactAddress;
            FullAddress = contactAddress.GetFullAddress();
        }
    }
}
