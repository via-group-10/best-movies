namespace BestMovies.Api.Models;

public class Rating
{
    public int MovieId { get; set; }
    public float Value { get; set; }
    public int Votes { get; set; }

    public virtual Movie Movie { get; set; } = null!;
}
