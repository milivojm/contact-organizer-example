using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactOrganizer.Web.Models
{
    public class ContactAddressModel : IContactAddress
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string StreetAndNumber { get; set; }
        public string GetFullAddress() { throw new NotImplementedException(); }
    }
}
