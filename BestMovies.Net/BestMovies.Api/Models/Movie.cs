namespace BestMovies.Api.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int? Year { get; set; }
    public List<Director> Directors { get; set; } = new List<Director>();
    public List<Star> Stars { get; set; } = new List<Star>();
    public List<Rating> Ratings { get; set; } = new List<Rating>();
}
