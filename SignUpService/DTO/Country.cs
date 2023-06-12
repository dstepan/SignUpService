namespace SignUpService.DTO;

public class Country
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Region[]? Regions { get; set; }
}

public class Region
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}