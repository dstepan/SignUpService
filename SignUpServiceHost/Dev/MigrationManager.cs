using Database.Context;
using Microsoft.EntityFrameworkCore;

namespace SignUpServiceHost.Dev;

public static class MigrationManager
{
    public static WebApplication MigrateDataDatabase(this WebApplication webApp)
    {
        using var scope = webApp.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<DataDbContext>();
        dbContext.Database.Migrate();
        return webApp;
    }

    public static WebApplication MigrateAccountDatabase(this WebApplication webApp)
    {
        using var scope = webApp.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<AccountDbContext>();
        dbContext.Database.Migrate();
        return webApp;
    }
}