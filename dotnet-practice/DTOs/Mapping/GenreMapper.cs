
namespace dotnet_practice.dtos.Mapping;

public static class GenreMapper
{
    public static GenreDto ToGenreDto(this Models.Genre genre)
    {
        return new GenreDto(
            genre.Id,
            genre.Name,
            genre.Games?.Select(g => g.ToGameDto()).ToList() ?? new List<GameDto>()
        );
    }
}
