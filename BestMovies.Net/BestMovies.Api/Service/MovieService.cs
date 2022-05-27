using BestMovies.Api.AppExtensions;
using BestMovies.Api.Integrations.Abstractions;
using BestMovies.Api.Integrations.Models;
using BestMovies.Api.Models;
using BestMovies.Api.Repository.Abstractions;
using BestMovies.Api.Service.Abstractions;

namespace BestMovies.Api.Service
{
    public class MovieService : IMovieService
    {
        private readonly ITmdbMoviesIntegrationsService tmdbMoviesIntegrations;
        private readonly IMovieRepository movieRepository;
        private readonly IConfiguration configuration;
        public MovieService(IConfiguration configuration, ITmdbMoviesIntegrationsService tmdbMoviesIntegrations, IMovieRepository movieRepository)
        {
            this.tmdbMoviesIntegrations = tmdbMoviesIntegrations;
            this.movieRepository = movieRepository;
            this.configuration = configuration;
        }
        public async Task<Movie?> GetMovieByIdAsync(int movieId)
        {
            TmdbMovie? tmdbMovie = await tmdbMoviesIntegrations.GetMovieByIdAsync(movieId);
            Movie? movie = await movieRepository.GetMovieByIdAsync(movieId);
            if(tmdbMovie == null)
            {
                return movie;
            }
            else
            {
            return TmdbMovieToMovie(movie!, tmdbMovie!);
            }
            
        }

        public async Task<List<Movie?>> GetMoviesByIdAsync(List<int> movieIds)
        {
            var results = new List<Movie?>();
            foreach(var movieId in movieIds)
            {
                results.Add(await GetMovieByIdAsync(movieId));
            }
            return results;
        }

        public Movie TmdbMovieToMovie(Movie movie, TmdbMovie tmdbMovie)
        {
            if (tmdbMovie.Id == movie.Id)
            {
                movie.ImageUrl = configuration.GetImageUrlPath() + tmdbMovie.ImageUrl;
                movie.Synopsis = tmdbMovie.Synopsis;
                movie.Year = tmdbMovie.releaseDate!.Value.Year;
            }
            return movie;
        }
    }
}
