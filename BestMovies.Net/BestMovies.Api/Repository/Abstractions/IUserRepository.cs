using BestMovies.Api.Models;

namespace BestMovies.Api.Repository.Abstractions
{
    public interface IUserRepository
    {
        Task<BestMoviesUser?> FindUserMatchAsync(string username, string password);
        Task<BestMoviesUser?> RegisterUserAsync(BestMoviesUser user);
        Task<BestMoviesUser?> GetUserByNameAsync(string name);
    }
}
