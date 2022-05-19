﻿namespace BestMovies.Api.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int? Year { get; set; }
}
