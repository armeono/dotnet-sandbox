


namespace dotnet_practice.dtos;

public record class GenreDto(
    int? Id,
    string Name,
    List<GameDto> Games
);