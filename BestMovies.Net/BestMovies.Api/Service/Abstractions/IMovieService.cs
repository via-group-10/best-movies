using BestMovies.Api.Integrations.Models;
using BestMovies.Api.Models;
using BestMovies.Api.Models.Filters;

namespace BestMovies.Api.Service.Abstractions
{
    public interface IMovieService
    {
        Task<Movie?> GetMovieByIdAsync(int movieId);
        Task<List<Movie>> GetMoviesByFilterAsync(MovieFilter filter);
        Movie TmdbMovieToMovie(Movie movie, TmdbMovie tmdbMovie);
        Task<List<Movie>> GetMyFavoriteListAsync(string username);
        Task<List<Movie>> GetFavoriteListAsync();
        Task<bool> AddFavoriteMovieToUserAsync(string username, int movieId);
        Task<bool> AddCommentToMovieAsync(int movieId, Comment comment);

    }
}
