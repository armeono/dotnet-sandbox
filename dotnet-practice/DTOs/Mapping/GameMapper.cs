using System;

namespace dotnet_practice.dtos.Mapping;

public static class GameMapper
{
    public static GameDto ToGameDto(this Models.Game game)
    {
        return new GameDto(
            game.Id,
            game.Name,
            game.Genre?.Name ?? "Unknown",
            game.Price,
            game.ReleaseDate
        );
    }
}
