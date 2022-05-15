namespace BestMovies.Api.Models;

public class Person
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public short? Birth { get; set; }
}
