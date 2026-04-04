namespace dotnet_practice.dtos;

public record UpdateGameDto(
    string Name, 
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
