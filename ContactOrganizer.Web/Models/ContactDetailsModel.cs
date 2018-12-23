using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactOrganizer.Web.Models
{
    public class ContactDetailsModel : IContactDetails
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TelephoneNumber { get; set; }
        public string StreetAndNumber { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}
