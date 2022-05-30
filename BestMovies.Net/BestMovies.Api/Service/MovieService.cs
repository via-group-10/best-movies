using BestMovies.Api.AppExtensions;
using BestMovies.Api.Integrations.Abstractions;
using BestMovies.Api.Integrations.Models;
using BestMovies.Api.Models;
using BestMovies.Api.Models.Filters;
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

        public async Task<List<Movie?>> GetMoviesByIdAsync(MovieFilter? filter)
        {
            if (filter == null)
            {
                filter = new MovieFilter();
            }
            List<Movie> movies = await movieRepository.GetMoviesAsync(filter);
            var results = new List<Movie?>();
            foreach(var movie in movies)
            {
                var tmdbMovie = await tmdbMoviesIntegrations.GetMovieByIdAsync(movie.Id);
                if (tmdbMovie == null)
                {
                    results.Add(movie);
                }
                results.Add(TmdbMovieToMovie(movie!, tmdbMovie!));
            }
            return results;
        }

        public Movie TmdbMovieToMovie(Movie movie, TmdbMovie tmdbMovie)
        {
            if (tmdbMovie.Id == movie.Id)
            {
                if (tmdbMovie.ImageUrl != null)
                {
                    movie.ImageUrl = configuration.GetImageUrlPath() + tmdbMovie.ImageUrl;
                }
                movie.Synopsis = tmdbMovie.Synopsis;
                movie.Year = tmdbMovie.releaseDate!.Value.Year;
            }
            return movie;
        }
    }
}
