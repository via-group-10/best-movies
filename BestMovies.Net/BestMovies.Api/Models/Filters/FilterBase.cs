namespace BestMovies.Api.Models.Filters;

public class FilterBase
{
    public int Limit { get; set; } = 15;
    public int Offset { get; set; } = 0;
}
