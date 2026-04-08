using System.ComponentModel.DataAnnotations;

namespace dotnet_practice.dtos;

public record class GameDto(
    int? Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);

public record UpdateGameDto(
    [Required][StringLength(50)] string Name,
    [Required] int GenreId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate
);

public record CreateGameDto(
    [Required][StringLength(50)] string Name,
    [Required] int GenreId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate
);
