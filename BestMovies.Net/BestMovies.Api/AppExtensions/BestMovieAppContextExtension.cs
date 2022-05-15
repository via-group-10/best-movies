using BestMovies.Api.Data;
using BestMovies.Api.Repository;
using BestMovies.Api.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BestMovies.Api.AppExtensions;

public static class BestMovieAppContextExtension
{
    public static IServiceCollection AddBestMoviesAppContext(this IServiceCollection services)
    {
        return services
            .AddDbContext<BestMoviesContext>(options =>
                options.UseSqlite("Data Source=movies.db"))
            .AddTransient<IMovieRepository, MovieRepository>();
    }
}
