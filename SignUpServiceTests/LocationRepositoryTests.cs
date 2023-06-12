using System.Diagnostics.CodeAnalysis;
using Database.Context;
using Database.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SignUpService.Repositories;
using SignUpService.Repositories.EF;

namespace SignUpServiceTests;

[ExcludeFromCodeCoverage]
[TestClass]
public class LocationRepositoryTests
{
    [TestMethod]
    public async Task CreateOrUpdateLocation_CreatesUpdatesAndGetsLocation()
    {
        const string accountId = "account";

        MockCountryRepository countryRepository = new ();
        CreateContextAndRepository(
            countryRepository,
            out var context, 
            out var repository);

        Location location;

        countryRepository.SetCountryAndRegion(Guid.NewGuid(), "Country", Guid.NewGuid(), "Region");
        await repository.CreateOrUpdateAccountLocationAsync(
            accountId,
            countryRepository.Region.CountryId,
            countryRepository.Region.Id);
        location = await repository.GetAccountLocationAsync(accountId);
        Assert.AreEqual(location.AccountId, accountId);
        Assert.AreEqual(location.CountryId, countryRepository.Region.CountryId);
        Assert.AreEqual(location.RegionId, countryRepository.Region.Id);

        countryRepository.SetCountryAndRegion(Guid.NewGuid(), "Country", Guid.NewGuid(), "Region");
        await repository.CreateOrUpdateAccountLocationAsync(
            accountId,
            countryRepository.Region.CountryId,
            countryRepository.Region.Id);
        location = await repository.GetAccountLocationAsync(accountId);
        Assert.AreEqual(location.AccountId, accountId);
        Assert.AreEqual(location.CountryId, countryRepository.Region.CountryId);
        Assert.AreEqual(location.RegionId, countryRepository.Region.Id);
    }

    [TestMethod]
    public async Task CreateOrUpdateLocation_ValidatesRegion()
    {
        MockCountryRepository countryRepository = new();
        CreateContextAndRepository(
            countryRepository,
            out var context,
            out var repository);

        await Assert.ThrowsExceptionAsync<ValidationException>(() => 
            repository.CreateOrUpdateAccountLocationAsync(
                "account",
                Guid.NewGuid(),
                Guid.NewGuid()));
    }

    [TestMethod]
    public async Task CreateOrUpdateLocation_ValidatesCountry()
    {
        MockCountryRepository countryRepository = new();
        CreateContextAndRepository(
            countryRepository,
            out var context,
            out var repository);

        countryRepository.SetCountryAndRegion(Guid.NewGuid(), "Country", Guid.NewGuid(), "Region");
        await Assert.ThrowsExceptionAsync<ValidationException>(() =>
            repository.CreateOrUpdateAccountLocationAsync(
                "account",
                Guid.NewGuid(),
                countryRepository.Region.Id));
    }

    [TestMethod]
    public async Task GetLocation_ValidatesAccount()
    {
        MockCountryRepository countryRepository = new();
        CreateContextAndRepository(
            countryRepository,
            out var context,
            out var repository);

        await Assert.ThrowsExceptionAsync<ValidationException>(() =>
            repository.GetAccountLocationAsync("account"));

        countryRepository.SetCountryAndRegion(Guid.NewGuid(), "Country", Guid.NewGuid(), "Region");
        await repository.CreateOrUpdateAccountLocationAsync(
            "account",
            countryRepository.Region.CountryId,
            countryRepository.Region.Id);
        await Assert.ThrowsExceptionAsync<ValidationException>(() =>
            repository.GetAccountLocationAsync("another_account"));
    }

    private static void CreateContextAndRepository(
        ICountryRepository countryRepository,
        out DataDbContext context,
        out ILocationRepository repository)
    {
        IDbContextFactory<DataDbContext> factory = new MockDataDbContextFactory();
        context = factory.CreateDbContext();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        repository = new EfLocationRepository(factory, countryRepository);
    }

    private class MockCountryRepository : ICountryRepository
    {
        public Country? Country { get; private set; }
        public Region? Region { get; private set; }

        public void SetCountryAndRegion(
            Guid countryId,
            string countryName,
            Guid regionId,
            string regionName)
        {
            Country = new Country
            {
                Id = countryId,
                Name = countryName
            };
            Region = new Region
            {
                Id = regionId,
                Name = regionName,
                CountryId = countryId
            };
            Country.Regions.Add(Region);
        }

        public Task<Country[]> GetCountriesAsync()
        {
            return Task.FromResult(Country != null 
                ? new []{ Country } 
                : Array.Empty<Country>());
        }

        public Task<Country> GetCountryAsync(Guid countryId, bool includeRegions)
        {
            if (Country == null || Country.Id != countryId)
            {
                throw new ValidationException("Region not available.");
            }

            return Task.FromResult(Country);
        }

        public Task<Region> GetRegionAsync(Guid regionId)
        {
            if (Region == null || Region.Id != regionId)
            {
                throw new ValidationException("Region not available.");
            }

            return Task.FromResult(Region);
        }
    }
}