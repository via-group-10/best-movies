using BestMovies.Api.Data;
using BestMovies.Api.Repository;
using BestMovies.Api.Repository.Abstractions;
using BestMovies.Api.Service;
using BestMovies.Api.Service.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BestMovies.Api.AppExtensions;

public static class BestMovieApiContextExtension
{
    public static IServiceCollection AddBestMoviesApiContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDbContext<BestMoviesContext>(options => options.UseSqlServer(configuration.GetConnectionString("MoviesDatabase")))
            .AddTransient<AuthenticationService>()
            .AddTransient<IMovieService, MovieService>()
            .AddTransient<IMovieRepository, MovieRepository>()
            .AddTransient<IUserRepository, UserRepository>()
            .AddBestMoviesAuthentication(configuration);
    }

    public static IServiceCollection AddBestMoviesAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.RequireHttpsMetadata = false;
               options.SaveToken = true;
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetValue<string>("JwtKey"))),
                   ValidateIssuer = false,
                    //Usually, this is your application base URL
                    // ValidIssuer = "http://localhost:45092/",
                   ValidateAudience = false,
                    //Here, we are creating and using JWT within the same application.
                    //In this case, base URL is fine.
                    //If the JWT is created using a web service, then this would be the consumer URL.
                    //ValidAudience = "http://localhost:45092/",
                   RequireExpirationTime = true,
                   ValidateLifetime = true,
                   ClockSkew = TimeSpan.Zero
               };
           });
        return services;
    }
}
