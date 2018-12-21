using System;
using System.Collections.Generic;
using System.Text;

namespace ContactOrganizer.Events
{
    public class ContactDetailsUpdated : IDomainEvent
    {
        public ContactDetailsUpdated(Contact contact)
        {
            Contact = contact;
        }

        public Contact Contact { get; }
    }
}
