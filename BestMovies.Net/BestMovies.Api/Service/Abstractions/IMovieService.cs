using BestMovies.Api.Integrations.Models;
using BestMovies.Api.Models;
using BestMovies.Api.Models.Filters;

namespace BestMovies.Api.Service.Abstractions
{
    public interface IMovieService
    {
        Task<Movie?> GetMovieByIdAsync(int movieId);
        Task<List<Movie?>> GetMoviesByIdAsync(MovieFilter filter);
        Movie TmdbMovieToMovie(Movie movie, TmdbMovie tmdbMovie);
    }
}
