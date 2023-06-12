using Database.Context;
using Database.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace SignUpService.Repositories.EF;

public class EfLocationRepository : ILocationRepository
{
    private readonly IDbContextFactory<DataDbContext> dbContextFactory;
    private readonly ICountryRepository countryRepository;

    public EfLocationRepository(
        IDbContextFactory<DataDbContext> dbContextFactory,
        ICountryRepository countryRepository)
    {
        this.dbContextFactory = dbContextFactory;
        this.countryRepository = countryRepository;
    }

    public async Task CreateOrUpdateAccountLocationAsync(
        string accountId,
        Guid countryId,
        Guid regionId)
    {
        Region region = await countryRepository.GetRegionAsync(regionId);
        if (region.CountryId != countryId)
        {
            throw new ValidationException("Specified region doesn't match specified country.");
        }

        await using var context = await dbContextFactory.CreateDbContextAsync();
        Location? location = await context.Locations.SingleOrDefaultAsync(e => e.AccountId == accountId);
        if (location != null)
        {
            location.CountryId = countryId;
            location.RegionId = regionId;
        }
        else
        {
            location = new Location
            {
                Id = Guid.NewGuid(),
                AccountId = accountId,
                CountryId = countryId,
                RegionId = regionId
            };
            await context.Locations.AddAsync(location);
        }
        await context.SaveChangesAsync();
    }

    public async Task<Location> GetAccountLocationAsync(string accountId)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();
        Location? location = await context.Locations.SingleOrDefaultAsync(e => e.AccountId == accountId);
        return location ?? throw new ValidationException("Location for account doesn't exist.");
    }
}