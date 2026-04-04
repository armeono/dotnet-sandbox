
using dotnet_practice.Data;
using dotnet_practice.dtos;
using dotnet_practice.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_practice.Services;

public class GameService
{

    private GameStoreContext db;
    private HttpClient httpClient;

    public GameService(GameStoreContext db, HttpClient httpClient)
    {
        this.db = db;
        this.httpClient = httpClient;
    }

    public async Task<List<Game>> GetGames()
    {
        var games = await db.Games.Include(g => g.Genre).ToListAsync();

        return games;

    }

    public async Task<Game> GetGameById(int id)
    {
        var game = await db.Games.Include(g => g.Genre).FirstOrDefaultAsync(g => g.Id == id);

        return game;
    }

    public async Task<Game> CreateGame(CreateGameDto requestGame)
    {
        var game = new Game
        {
            Name = requestGame.Name,
            GenreId = requestGame.GenreId,
            Price = requestGame.Price,
            ReleaseDate = requestGame.ReleaseDate
        };

        db.Games.Add(game);

        await db.SaveChangesAsync();

        return game;

    }

    public async Task<bool> UpdateGame(int id, UpdateGameDto updatedGame)
    {

        var game = await db.Games.FindAsync(id);


        if (game is null)
        {
            return false;
        }

        game.Name = updatedGame.Name;
        game.Price = updatedGame.Price;
        game.ReleaseDate = updatedGame.ReleaseDate;


        await db.SaveChangesAsync();

        return true;

    }


}
