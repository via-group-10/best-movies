using BestMovies.Api.Data;
using BestMovies.Api.Data.Extensions;
using BestMovies.Api.Models;
using BestMovies.Api.Models.Filters;
using BestMovies.Api.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BestMovies.Api.Repository;

public class MovieRepository : RepositoryBaseQueryable<Movie>, IMovieRepository
{

    public MovieRepository(BestMoviesContext context) : base(context)
    {
        baseQuery = baseQuery
            .Include(_ => _.Rating)
            .Include(_ => _.Stars).ThenInclude(_ => _.Person)
            .Include(_ => _.Directors).ThenInclude(_ => _.Person)
            .Include(_ => _.FavoredByUsers)
            .Include(_ => _.Comments);
    }

    public async Task<Movie?> GetMovieByIdAsync(int id)
    {
        return await baseQuery.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<List<Movie>> GetMoviesAsync(MovieFilter filter)
    {
        IQueryable<Movie> query = baseQuery;

        if (filter.Title is not null)
        {
            query = baseQuery.Where(m => EF.Functions.Like(m.Title, $"%{filter.Title}%"));
        }

        return await query.Sort(filter).ToListAsync();
    }

    public async Task<bool> AddCommentToMovieAsync(int movieId, Comment comment)
    {
        Task<Movie?> movieTask = Context.Movies
            .Include(m => m.Comments)
            .FirstOrDefaultAsync(m => m.Id == movieId);

        var movie = await movieTask;

        if (movie == null)
        {
            return false;
        }

        movie.Comments.Add(comment);

        int changes = await Context.SaveChangesAsync();

        return changes > 0;
    }
}
