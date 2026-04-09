namespace dotnet_practice.Models;

public class Genre
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public List<Game> Games { get; set; } = [];

}
