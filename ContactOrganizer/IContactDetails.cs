using System;

namespace ContactOrganizer
{
    public interface IContactDetails
    {
        string FirstName { get; }
        string LastName { get; }
        IContactAddress Address { get; }
        string TelephoneNumber { get; }        
    }
}