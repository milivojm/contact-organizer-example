namespace ContactOrganizer
{
    /// <summary>
    /// Interface representing a contact address allowing future extensions to include lists of countries, cities or full address model.
    /// </summary>
    public interface IContactAddress
    {
        string City { get; }
        string Country { get; }
        string PostalCode { get; }
        string StreetAndNumber { get; }
        string GetFullAddress();
    }
}