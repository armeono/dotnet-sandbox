using dotnet_practice.Data;
using dotnet_practice.dtos.Mapping;
using dotnet_practice.Models;
using dotnet_practice.Services;
using Microsoft.EntityFrameworkCore;

namespace dotnet_practice.endpoints;

public static class GenresEndpoints
{

    public static void MapGenresEndpoints(this WebApplication app)
    {

        var group = app.MapGroup("/genres");

        group.MapGet("/", async (GenreService genreService, int page = 1, int pageSize = 10) =>
        {

            if (page <= 0 || pageSize <= 0)
            {
                return Results.BadRequest("Page and pageSize must be greater than 0.");
            }

            var genres = await genreService.GetGenres(page, pageSize);


            return Results.Ok(genres.Select(g => GenreMapper.ToGenreDto(g)).ToList());

        });

        group.MapPost("/", async (GameStoreContext db, Genre genre) =>
        {
            db.Genres.Add(genre);

            await db.SaveChangesAsync();

            return Results.Created($"/genres/{genre.Id}", genre.Id);

        });

    }

}
