using System;
using System.Text;

namespace ContactOrganizer
{
    /// <summary>
    /// A simple implementation of IContactAddress interface using string fields.
    /// </summary>
    public class ContactAddress : IContactAddress
    {
        private string _streetAndNumber;
        private string _city;

        public ContactAddress(string streetAndNumber, string city, string postalCode, string country)
        {
            StreetAndNumber = streetAndNumber;
            City = city;
            PostalCode = postalCode;
            Country = country;
        }

        public string StreetAndNumber
        {
            get => _streetAndNumber;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Street and house number cannot be empty.");

                _streetAndNumber = value;
            }
        }

        public string City
        {
            get => _city;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("City cannot be empty.");

                _city = value;
            }
        }

        public string PostalCode { get; }

        public string Country { get; }

        public string GetFullAddress()
        {
            StringBuilder addressStringBuilder = new StringBuilder();
            addressStringBuilder.AppendLine(StreetAndNumber);

            if (!string.IsNullOrEmpty(PostalCode))
                addressStringBuilder.AppendFormat("{0} {1}", PostalCode, City);
            else
                addressStringBuilder.Append(City);

            if (!string.IsNullOrEmpty(Country))
            {
                addressStringBuilder.AppendLine();
                addressStringBuilder.Append(Country);
            }

            return  addressStringBuilder.ToString();
        }

        private void SetFullAddress()
        {
            
        }
    }
}