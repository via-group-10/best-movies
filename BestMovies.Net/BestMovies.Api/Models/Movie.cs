namespace BestMovies.Api.Models;

public class Movie
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public short? Year { get; set; }
}
