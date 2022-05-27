using BestMovies.Api.Integrations.Models;

namespace BestMovies.Api.Integrations.Abstractions
{
    public interface ITmdbMoviesIntegrations
    {
        Task<TmdbMovie?> GetMovieAsync(int movieId);
        Task<List<TmdbMovie?>> GetMoviesAsync(List<int> movieIds);
    }
}
