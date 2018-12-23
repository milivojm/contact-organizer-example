using System;

namespace ContactOrganizer
{
    /// <summary>
    /// Represents form of input data for the contact organizer app. This makes validation possible in the business core, instead in different client apps.
    /// </summary>
    public interface IContactDetails
    {
        string FirstName { get; }
        string LastName { get; }
        string City { get; }
        string Country { get; }
        string PostalCode { get; }
        string StreetAndNumber { get; }
        string TelephoneNumber { get; }        
    }
}