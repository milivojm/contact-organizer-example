using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactOrganizer.Web.Models
{
    public class ContactDetailsModel : IContactDetails 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IContactAddress Address { get; set; }
        public string TelephoneNumber { get; set; }
        public Guid Id { get; set; }
    }
}
