using Database.Entities;

namespace SignUpService.Repositories;

/// <summary>
/// Manages user locations.
/// </summary>
public interface ILocationRepository
{
    /// <summary>
    /// Creates or updates location for specified account.
    /// </summary>
    /// <remarks>
    /// Note that the method doesn't validate account with specified ID exists.
    /// </remarks>
    /// <param name="accountId">User account ID.</param>
    /// <param name="countryId">New user country ID.</param>
    /// <param name="regionId">New user region ID.</param>
    /// <exception cref="FluentValidation.ValidationException">
    /// Is thrown if specified region doesn't exist or doesn't match specified country.
    /// </exception>
    public Task CreateOrUpdateAccountLocationAsync(
        string accountId,
        Guid countryId,
        Guid regionId);

    /// <summary>
    /// Gets location for specified account.
    /// </summary>
    /// <param name="accountId">User account ID.</param>
    /// <exception cref="FluentValidation.ValidationException">
    /// Is thrown if no location for specified account exists.
    /// </exception>
    public Task<Location> GetAccountLocationAsync(string accountId);
}