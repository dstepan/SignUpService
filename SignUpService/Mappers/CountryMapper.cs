namespace SignUpService.Mappers;

public static class CountryMapper
{
    public static DTO.Country ToDto(
        Database.Entities.Country country,
        bool includeRegions = false)
    {
        return new DTO.Country
        {
            Id = country.Id,
            Name = country.Name,
            Regions = includeRegions
                ? country.Regions.Select(ToDto).ToArray()
                : null
        };
    }

    public static DTO.Region ToDto(Database.Entities.Region region)
    {
        return new DTO.Region
        {
            Id = region.Id,
            Name = region.Name
        };
    }
}