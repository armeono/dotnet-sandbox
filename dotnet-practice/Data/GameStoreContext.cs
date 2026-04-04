using dotnet_practice.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_practice.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();

}
