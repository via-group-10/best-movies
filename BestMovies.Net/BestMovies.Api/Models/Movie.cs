using System.ComponentModel.DataAnnotations.Schema;

namespace BestMovies.Api.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    [NotMapped]
    public int? Year { get; set; }
    [NotMapped]
    public string? Synopsis { get; set; }
    [NotMapped]
    public string? ImageUrl { get; set; }
    
    public List<Director> Directors { get; set; } = new List<Director>();
    public List<Star> Stars { get; set; } = new List<Star>();
    public Rating Rating { get; set; }
    public List<Comment> Comments { get; set; }

    public List<UserFavoriteMovie> FavoredByUsers { get; set; } = new List<UserFavoriteMovie>();
}
