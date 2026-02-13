using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UrlShortener.Web.Data;

/// <summary>
/// Allows EF Core CLI tools (dotnet ef migrations) to create the DbContext
/// without starting the application or requiring a live database connection.
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // Dummy connection string for migration generation only — never used at runtime.
        optionsBuilder.UseMySql(
            "Server=localhost;Database=urlshortener",
            ServerVersion.Create(8, 0, 0, Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MySql));

        return new AppDbContext(optionsBuilder.Options);
    }
}
