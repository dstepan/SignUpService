using Database.Context;
using Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SignUpService.Repositories;
using SignUpService.Repositories.EF;
using SignUpService.Repositories.Identity;
using SignUpServiceHost.Dev;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AccountDbContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("AccountDb")));
builder.Services.AddIdentity<Account, IdentityRole>(
        options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireDigit = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;
        })
    .AddEntityFrameworkStores<AccountDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthorization();

builder.Services.AddDbContextFactory<DataDbContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("DataDb")));

builder.Services.AddScoped<IAccountRepository, IdentityAccountRepository>();
builder.Services.AddScoped<ICountryRepository, EfCountryRepository>();
builder.Services.AddScoped<ILocationRepository, EfLocationRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();

app.MapControllers();

if (builder.Environment.IsDevelopment())
{
    app.MigrateAccountDatabase();
    app.MigrateDataDatabase();
}

app.Run();
