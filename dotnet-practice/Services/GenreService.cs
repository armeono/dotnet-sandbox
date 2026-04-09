
using dotnet_practice.Data;
using dotnet_practice.Models;
using Microsoft.EntityFrameworkCore;


namespace dotnet_practice.Services;

public class GenreService
{

    private GameStoreContext db;
    private HttpClient httpClient;

    public GenreService(GameStoreContext db, HttpClient httpClient)
    {
        this.db = db;
        this.httpClient = httpClient;
    }


    public async Task<List<Genre>> GetGenres(int page, int pageSize)
    {
        var genres = await db.Genres.Include(g => g.Games).OrderBy(g => g.Id).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();


        return genres;
    }




}
