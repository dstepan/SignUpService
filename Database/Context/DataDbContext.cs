using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Context;

public class DataDbContext : DbContext
{
    public DbSet<Country> Countries { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Location> Locations { get; set; }

    public DataDbContext(DbContextOptions<DataDbContext> options) :
        base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>()
            .Property(e => e.Name)
            .IsRequired();
        modelBuilder.Entity<Country>()
            .HasIndex(e => e.Name)
            .IsUnique(false);

        modelBuilder.Entity<Region>()
            .Property(e => e.Name)
            .IsRequired();
        modelBuilder.Entity<Region>()
            .HasOne<Country>()
            .WithMany(e => e.Regions)
            .HasForeignKey(e => e.CountryId)
            .IsRequired();

        modelBuilder.Entity<Location>()
            .HasIndex(e => e.AccountId)
            .IsUnique();
        modelBuilder.Entity<Location>()
            .Property(e => e.RegionId)
            .IsRequired();
        modelBuilder.Entity<Location>()
            .Property(e => e.CountryId)
            .IsRequired();

        CreateCountries(out var countries, out var regions);
        modelBuilder.Entity<Country>().HasData(countries);
        modelBuilder.Entity<Region>().HasData(regions);
    }

    private static void CreateCountries(
        out Country[] countries,
        out Region[] regions)
    {
        List<Country> countryList = new();
        List<Region> regionList = new();
        CreateCountry(
            "Brazil",
            new[]
            {
                "North",
                "Northeast",
                "Central-West",
                "Southeast",
                "South"
            },
            ref countryList,
            ref regionList);
        CreateCountry(
            "South Africa",
            new[]
            {
                "Gauteng",
                "Mpumalanga",
                "KwaZulu-Natal",
                "North West",
                "Limpopo",
                "Western Cape",
                "Free State",
                "Eastern Cape",
                "Northern Cape",
                "South Africa"
            },
            ref countryList,
            ref regionList);
        countries = countryList.ToArray();
        regions = regionList.ToArray();
    }

    private static void CreateCountry(
        string countryName, 
        string[] regionNames,
        ref List<Country> countries,
        ref List<Region> regions)
    {
        Guid countryId = Guid.NewGuid();
        countries.Add(new Country
        {
            Id = countryId,
            Name = countryName
        });
        regions.AddRange(
            regionNames.Select(regionName => new Region
            {
                Id = Guid.NewGuid(),
                CountryId = countryId,
                Name = regionName
            }));
    }
}