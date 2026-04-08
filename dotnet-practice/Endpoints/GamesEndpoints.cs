using dotnet_practice.Data;
using dotnet_practice.dtos;
using dotnet_practice.dtos.Mapping;
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
            var games = await gameService.GetGames();


            return games.Select(g => GameMapper.ToGameDto(g)).ToList();
        });

        group.MapGet("/{id}", async (int id, GameService gameService) =>
        {
            var game = await gameService.GetGameById(id);

            if (game is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(GameMapper.ToGameDto(game));
        }).WithName(GetGameByIdName);

        group.MapPost("/", async (GameService gameService, CreateGameDto requestGame) =>
        {
            var game = await gameService.CreateGame(requestGame);

            return Results.Created($"/games/{game.Id}", GameMapper.ToGameDto(game));

        });

        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameService gameService) =>
        {
            var result = await gameService.UpdateGame(id, updatedGame);

            if (result is false)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, GameService gameService) =>
        {
            var result = await gameService.DeleteGame(id);

            if (result is false)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        });


    }

}
