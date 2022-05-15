﻿using BestMovies.Api.Data;
using BestMovies.Api.Data.Extensions;
using BestMovies.Api.Models;
using BestMovies.Api.Models.Filters;
using BestMovies.Api.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BestMovies.Api.Repository;

public class MovieRepository : RepositoryBase, IMovieRepository
{

    public MovieRepository(BestMoviesContext context) : base(context)
    {

    }

    public async Task<List<Movie>> GetMoviesAsync(MovieFilter filter)
    {
        return await Context.Movies.Sort(filter).ToListAsync();
    }
}