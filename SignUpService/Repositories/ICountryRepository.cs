using Database.Entities;

namespace SignUpService.Repositories;

/// <summary>
/// Manages country information.
/// </summary>
public interface ICountryRepository
{
    /// <summary>
    /// Lists all available countries.
    /// </summary>
    public Task<Country[]> GetCountriesAsync();

    /// <summary>
    /// Gets specified country. optionally with its regions.
    /// </summary>
    /// <param name="countryId">Country ID.</param>
    /// <param name="includeRegions">Indicates whether country regions should be fetched.</param>
    /// <exception cref="FluentValidation.ValidationException">
    /// Is thrown if specified country doesn't exist.
    /// </exception>
    public Task<Country> GetCountryAsync(
        Guid countryId,
        bool includeRegions);

    /// <summary>
    /// Get specified region.
    /// </summary>
    /// <param name="regionId">Region ID.</param>
    /// <exception cref="FluentValidation.ValidationException">
    /// Is thrown if specified region doesn't exist.
    /// </exception>
    public Task<Region> GetRegionAsync(Guid regionId);
}
