using Database.Context;
using Database.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace SignUpService.Repositories.EF;

public class EfCountryRepository : ICountryRepository
{
    private readonly IDbContextFactory<DataDbContext> dbContextFactory;

    public EfCountryRepository(
        IDbContextFactory<DataDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<Country[]> GetCountriesAsync()
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();
        Country[] result = await context.Countries
            .OrderBy(e => e.Name)
            .ToArrayAsync();
        return result;
    }

    public async Task<Country> GetCountryAsync(
        Guid countryId,
        bool includeRegions)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();
        Country? country = await context.Countries.SingleOrDefaultAsync(e => e.Id == countryId);
        if (country != null && includeRegions)
        {
            await context.Entry(country).Collection(e => e.Regions).LoadAsync();
        }
        return country ?? throw new ValidationException("Country with specified ID doesn't exist.");
    }

    public async Task<Region> GetRegionAsync(Guid regionId)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();
        Region? region = await context.Regions.SingleOrDefaultAsync(e => e.Id == regionId);
        return region ?? throw new ValidationException("Region with specified ID doesn't exist.");
    }
}