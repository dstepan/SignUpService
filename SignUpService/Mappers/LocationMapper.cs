namespace SignUpService.Mappers;

public static class LocationMapper
{
    public static DTO.Location ToDto(Database.Entities.Location location)
    {
        return new DTO.Location
        {
            CountryId = location.CountryId,
            RegionId = location.RegionId
        };
    }
}