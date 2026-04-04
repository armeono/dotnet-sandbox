
using Microsoft.EntityFrameworkCore;

namespace dotnet_practice.Data;

public static class DataExtensions
{

    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        dbContext.Database.Migrate();
    }

    public static void ConnectDb(this WebApplicationBuilder builder)
    {
        var connString = builder.Configuration.GetConnectionString("DBKey");
        builder.Services.AddSqlite<GameStoreContext>(connString);
    }

}
