using System;
using dotnet_practice.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace dotnet_practice.Tests.Services;

public class GenreServiceTests
{
    private static async Task<GameStoreContext> CreateDbContext()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        await connection.OpenAsync();

        var options = new DbContextOptionsBuilder<GameStoreContext>()
            .UseSqlite(connection)
            .Options;

        var context = new GameStoreContext(options);
        await context.Database.EnsureCreatedAsync();

        return context;
    }

}
