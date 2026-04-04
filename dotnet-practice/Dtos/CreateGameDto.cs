using System.ComponentModel.DataAnnotations;

namespace dotnet_practice.dtos;

public record CreateGameDto(
    [Required][StringLength(50)] string Name,
    [Required] int GenreId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate
);
