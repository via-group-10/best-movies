using BestMovies.Api.Integrations.Models;

namespace BestMovies.Api.Integrations.Abstractions
{
    public interface ITmdbMoviesIntegrationsService
    {
        Task<TmdbMovie?> GetMovieByIdAsync(int movieId);
        Task<List<TmdbMovie?>> GetMoviesByIdAsync(List<int> movieIds);
    }
}
