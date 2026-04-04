using dotnet_practice.Data;
using dotnet_practice.dtos;
using dotnet_practice.Models;
using dotnet_practice.Services;
using Microsoft.EntityFrameworkCore;

namespace dotnet_practice.endpoints;

public static class GamesEndpoints
{

    const string GetGameByIdName = "GetGameById";


    public static void MapGamesEndpoints(this WebApplication app)
    {

        var group = app.MapGroup("/games");

        group.MapGet("/", async (GameService gameService) =>
        {
            return await gameService.GetGames();
        });

        group.MapGet("/{id}", async (int id, GameService gameService) =>
        {
            var game = await gameService.GetGameById(id);

            return Results.Ok(game);
        }).WithName(GetGameByIdName);

        group.MapPost("/", async (GameService gameService, CreateGameDto requestGame) =>
        {
            var game = await gameService.CreateGame(requestGame);

            return Results.Created($"/games/{game.Id}", game);

        });

        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameService gameService) =>
        {
            await gameService.UpdateGame(id, updatedGame);

            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) =>
        {

            // Implement delete


            return Results.NoContent();
        });


    }

}
