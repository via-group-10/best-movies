namespace BestMovies.Api.Models;

public class Rating
{
    public long MovieId { get; set; }
    public double Value { get; set; }
    public long Votes { get; set; }

    public virtual Movie Movie { get; set; } = null!;
}
