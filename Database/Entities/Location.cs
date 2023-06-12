namespace Database.Entities;

public class Location
{
    public Guid Id { get; set; }
    public string AccountId { get; set; }
    public Guid CountryId { get; set; }
    public Guid RegionId { get; set; }
}
