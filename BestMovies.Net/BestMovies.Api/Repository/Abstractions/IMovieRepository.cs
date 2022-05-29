using BestMovies.Api.Models;
using BestMovies.Api.Models.Filters;

namespace BestMovies.Api.Repository.Abstractions;

public interface IMovieRepository
{
    Task<List<Movie>> GetMoviesAsync(MovieFilter filter);
    Task<Movie?> GetMovieByIdAsync(int id);
    Task<bool> AddCommentToMovieAsync(int movieId, Comment comment);
    Task<bool> AddFavoriteMovieToUserAsync(string username, int movieId);
    Task<List<Movie>> GetMyFavoriteListAsync(string username);
    Task<List<Movie>> GetFavoriteListAsync();
}
