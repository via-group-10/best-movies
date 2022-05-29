using BestMovies.Api.Data;
using BestMovies.Api.Models;
using BestMovies.Api.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BestMovies.Api.Repository
{
    public class UserRepository : RepositoryBaseQueryable<BestMoviesUser>, IUserRepository
    {
        public UserRepository(BestMoviesContext context) : base(context)
        {

        }

        public async Task<BestMoviesUser?> FindUserMatchAsync(string username, string password)
        {
            bool userExists = baseQuery.Any(u => u.Name == username);
            if (!userExists)
            {
                return null;
            }
            else 
            {
                return await baseQuery.FirstOrDefaultAsync(u => u.Name == username && u.Pass == password);
            }
        }

        public async Task<BestMoviesUser?> GetUserByNameAsync(string name)
        {
            return await baseQuery.FirstOrDefaultAsync(u => u.Name == name);
        }

        public async Task<BestMoviesUser?> RegisterUserAsync(BestMoviesUser user)
        {
            bool userExists = baseQuery.Any(u => u.Name == user.Name);
            if (userExists)
            {
                return null;
            }
            else
            {
                var newUser = await Context.Users.AddAsync(user);
                await Context.SaveChangesAsync();
                return await baseQuery.FirstOrDefaultAsync(u => u.Id == newUser.Entity.Id);
            }

        }
    }
}
