using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactOrganizer.Web.Models
{
    public class SearchContactsModel
    {
        public int TotalRows { get; set; }
        public int StartFrom { get; set; }
        public List<Contact> Result { get; set; }
    }
}
