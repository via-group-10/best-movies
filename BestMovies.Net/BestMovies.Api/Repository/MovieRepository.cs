using BestMovies.Api.Data;
using BestMovies.Api.Data.Extensions;
using BestMovies.Api.Exceptions;
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
            .Include(_ => _.Directors).ThenInclude(_ => _.Person);
    }

    public async Task<Movie?> GetMovieByIdAsync(int id)
    {
        var movie = await baseQuery
            .Include(_ => _.FavoredByUsers)
            .Include(_ => _.Comments)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie != null)
        {
            movie.Comments = movie.Comments.OrderByDescending(c => c.Created).ToList();
            movie.FavoredByUsers.ForEach(um => um.User = null!);
            return movie;
        }
        else
            return null;
    }

    public async Task<List<Movie>> GetMoviesAsync(MovieFilter filter)
    {
        IQueryable<Movie> query = baseQuery;

        if (filter.Title != null)
        {
            string filterTitle = $"%{filter.Title}%";
            query = baseQuery.Where(m => EF.Functions.Like(m.Title, filterTitle));
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

    public async Task<bool> AddFavoriteMovieToUserAsync(string username, int movieId)
    {
        int changes = 0;
        var user = await Context.Users.Include(_ => _.FavoriteMovies).FirstOrDefaultAsync(u => u.Name == username);
        var movie = await Context.Movies.FindAsync(movieId);


        if (user == null || user.FavoriteMovies.Any(um => um.MovieId == movieId))
        {
            throw new RecordAlreadyExistsException(typeof(Movie));
        }


        if (movie == null)
        {
            return false;
        }

        user.FavoriteMovies.Add(new UserFavoriteMovie()
        {
            Movie = movie
        });

        changes = await Context.SaveChangesAsync();

        return changes > 0;
    }

    public async Task<List<Movie>> GetMyFavoriteListAsync(string username)
    {
        var user = await Context.Users
            .FirstOrDefaultAsync(u => u.Name == username);

        if (user == null)
        {
            return new List<Movie>();
        }

        var movies = await Context.UserFavoriteMovies
            .Include(_ => _.Movie).ThenInclude(_ => _.Rating)
            .Include(_ => _.Movie).ThenInclude(_ => _.Stars).ThenInclude(_ => _.Person)
            .Include(_ => _.Movie).ThenInclude(_ => _.Directors).ThenInclude(_ => _.Person)
            .Where(fm => fm.UserId == user.Id)
            .Select(fm => fm.Movie)
            .RemoveOrdering()
            .ToListAsync();


        return movies;
    }

    public async Task<List<Movie>> GetFavoriteListAsync()
    {
        var movies = await baseQuery
            .Where(m => m.FavoredByUsers.Any()).Take(15).ToListAsync();
        return movies;
    }
}
