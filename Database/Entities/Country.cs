namespace Database.Entities;

public class Country
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Region> Regions { get; } = new List<Region>();
}

public class Region
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Guid CountryId { get; set; }
}