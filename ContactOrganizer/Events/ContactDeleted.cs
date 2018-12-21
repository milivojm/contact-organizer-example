using System;
using System.Collections.Generic;
using System.Text;

namespace ContactOrganizer.Events
{
    public class ContactDeleted : IDomainEvent
    {
        public ContactDeleted(Guid contactId)
        {
            Contact = contactId;
        }

        public Guid Contact { get; }
    }
}
