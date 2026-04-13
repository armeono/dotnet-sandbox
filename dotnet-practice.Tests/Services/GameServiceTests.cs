using System;
using dotnet_practice.Data;
using dotnet_practice.dtos;
using dotnet_practice.Models;
using dotnet_practice.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace dotnet_practice.Tests.Services;

public class GameServiceTests
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

    private static async Task SeedData(GameStoreContext db)
    {
        db.Games.AddRange(
            new Game
            {
                Id = 1,
                Name = "Halo",
                GenreId = 1,
                Price = 50,
                ReleaseDate = new DateOnly(2024, 1, 1)
            },
            new Game
            {
                Id = 2,
                Name = "Doom",
                GenreId = 1,
                Price = 40,
                ReleaseDate = new DateOnly(2024, 2, 1)
            },
               new Game
               {
                   Id = 3,
                   Name = "Resident Evil",
                   GenreId = 1,
                   Price = 60,
                   ReleaseDate = new DateOnly(2024, 3, 1)
               }
        );

        await db.SaveChangesAsync();
    }

    [Fact]
    public async Task GetGamesById_ReturnsNull_WhenGameDoesNotExist()
    {
        var db = await CreateDbContext();
        await SeedData(db);

        var httpClient = new HttpClient();

        var service = new GameService(db, httpClient);

        var result = await service.GetGameById(999);

        Assert.Null(result);
    }


    [Fact]
    public async Task CreateGame_ReturnsGame_WhenGameIsCreated()
    {
        var db = await CreateDbContext();
        await SeedData(db);

        var httpClient = new HttpClient();

        var service = new GameService(db, httpClient);


        await service.CreateGame(new CreateGameDto(
         "Spiderman",
         1,
         80,
         new DateOnly(2024, 1, 1)
        ));


        var result = await service.GetGameById(4);


        Assert.Equal("Spiderman", result?.Name);
    }


}
