using dotnet_practice.Data;
using dotnet_practice.dtos;
using dotnet_practice.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_practice.endpoints;

public static class GamesEndpoints
{

    const string GetGameByIdName = "GetGameById";

    private static readonly List<GameDto> games = new List<GameDto>
    {
        new (1, "RDR2", "Adventure", 50, new DateOnly(2018, 10, 26)),
        new (2, "GTA6", "Action", 60, new DateOnly(2024, 11, 5)),
        new (3, "Resident Evil", "Horror", 40, new DateOnly(2023, 1, 15))
    };

    public static void MapGamesEndpoints(this WebApplication app)
    {

        var group = app.MapGroup("/games");

        group.MapGet("/", async (GameStoreContext db) =>
        {
            var games = await db.Games.Include(g => g.Genre).ToListAsync();

            return games;
        });

        group.MapGet("/{id}", (int id) =>
        {

            var game = games.Find(game => game.Id == id);

            if (game is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(game);
        }).WithName(GetGameByIdName);

        group.MapPost("/", async (GameStoreContext db, CreateGameDto requestGame) =>
        {
            var game = new Game
            {
                Name = requestGame.Name,
                GenreId = requestGame.GenreId,
                Price = requestGame.Price,
                ReleaseDate = requestGame.ReleaseDate
            };

            db.Games.Add(game);

            await db.SaveChangesAsync();

            return Results.Created($"/games/{game.Id}", game);

        });

        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var gameIndex = games.FindIndex(game => game.Id == id);

            if (gameIndex is -1)
            {
                return Results.NotFound();
            }

            games[gameIndex] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate

            );

            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) =>
        {

            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });


    }

}
