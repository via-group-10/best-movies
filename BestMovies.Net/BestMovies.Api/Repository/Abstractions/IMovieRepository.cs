using BestMovies.Api.Models;
using BestMovies.Api.Models.Filters;

namespace BestMovies.Api.Repository.Abstractions;

public interface IMovieRepository
{
    public Task<List<Movie>> GetMoviesAsync(MovieFilter filter);
}
