using BestMovies.Api.Models;
using BestMovies.Api.Models.Filters;

namespace BestMovies.Api.Repository.Abstractions;

public interface IMovieRepository
{
    Task<List<Movie>> GetMoviesAsync(MovieFilter filter);
    Task<Movie?> GetMovieByIdAsync(int id);
}
