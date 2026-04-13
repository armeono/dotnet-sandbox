using dotnet_practice.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace dotnet_practice.Tests.Infrastructure;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    private SqliteConnection? connection;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<GameStoreContext>));

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            services.AddDbContext<GameStoreContext>(options =>
            {
                options.UseSqlite(connection);
            });

            var serviceProvider = services.BuildServiceProvider();

            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

            db.Database.EnsureDeleted();

        });
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        connection?.Dispose();
    }
}
