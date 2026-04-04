using dotnet_practice.Data;
using dotnet_practice.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_practice.endpoints;

public static class GenresEndpoints
{

    public static void MapGenresEndpoints(this WebApplication app)
    {

        var group = app.MapGroup("/genres");

        group.MapGet("/", async (GameStoreContext db) =>
        {
            var genres = await db.Genres.ToListAsync();

            return genres;

        });

        group.MapPost("/", async (GameStoreContext db, Genre genre) =>
        {
            db.Genres.Add(genre);

            await db.SaveChangesAsync();

            return Results.Created($"/genres/{genre.Id}", genre.Id);

        });

    }

}
