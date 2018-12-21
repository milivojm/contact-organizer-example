namespace ContactOrganizer
{
    public interface IContactAddress
    {
        string City { get; }
        string Country { get; }
        string PostalCode { get; }
        string StreetAndNumber { get; }
    }
}