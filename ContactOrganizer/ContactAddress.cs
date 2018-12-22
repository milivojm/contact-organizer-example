using System;
using System.Text;

namespace ContactOrganizer
{
    public class ContactAddress : IContactAddress
    {
        private string _streetAndNumber;
        private string _city;
        private string _fullAddress;

        public ContactAddress(string streetAndNumber, string city, string postalCode, string country)
        {
            StreetAndNumber = streetAndNumber;
            City = city;
            PostalCode = postalCode;
            Country = country;
            SetFullAddress();
        }

        public ContactAddress(IContactAddress contactAddress)
        {
            StreetAndNumber = contactAddress.StreetAndNumber;
            City = contactAddress.City;
            PostalCode = contactAddress.PostalCode;
            Country = contactAddress.Country;
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
            return _fullAddress;
        }

        private void SetFullAddress()
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

            _fullAddress = addressStringBuilder.ToString();
        }
    }
}