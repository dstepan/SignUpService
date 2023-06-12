using Database.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Database.Context;

public class AccountDbContext : IdentityDbContext<Account>
{
    public AccountDbContext(DbContextOptions<AccountDbContext> options) : 
        base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}