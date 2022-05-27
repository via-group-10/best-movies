using BestMovies.Api.Integrations.Models;
using BestMovies.Api.Models;

namespace BestMovies.Api.Service.Abstractions
{
    public interface IMovieService
    {
        Task<Movie?> GetMovieByIdAsync(int movieId);
        Task<List<Movie?>> GetMoviesByIdAsync(List<int> movieIds);
        Movie TmdbMovieToMovie(Movie movie, TmdbMovie tmdbMovie);
    }
}
