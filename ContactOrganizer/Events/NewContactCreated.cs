using System;
using System.Collections.Generic;
using System.Text;

namespace ContactOrganizer.Events
{
    public class NewContactCreated : IDomainEvent
    {
        public NewContactCreated(Contact contact)
        {
            Contact = contact;
        }

        public Contact Contact { get; }
    }
}
