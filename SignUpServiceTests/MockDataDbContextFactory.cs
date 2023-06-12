using System.Diagnostics.CodeAnalysis;
using Database.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace SignUpServiceTests;

[ExcludeFromCodeCoverage]
public class MockDataDbContextFactory : IDbContextFactory<DataDbContext>
{
    private readonly DbContextOptions<DataDbContext> options;
    
    public MockDataDbContextFactory()
    {
        var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();
        options = new DbContextOptionsBuilder<DataDbContext>()
            .UseSqlite(connection)
            .Options;
    }

    DataDbContext IDbContextFactory<DataDbContext>.CreateDbContext()
    {
        return new DataDbContext(options);
    }
}