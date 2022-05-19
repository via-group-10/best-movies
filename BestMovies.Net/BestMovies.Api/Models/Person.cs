namespace BestMovies.Api.Models;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? Birth { get; set; }
}
