using BestMovies.Api.Integrations.Abstractions;
using BestMovies.Api.Integrations.Models;
using BestMovies.Api.Models;
using BestMovies.Api.Service.Abstractions;

namespace BestMovies.Api.Service
{
    public class MovieService : IMovieService
    {
        private readonly ITmdbMoviesIntegrations tmdbMoviesIntegrations;
        public MovieService(ITmdbMoviesIntegrations tmdbMoviesIntegrations)
        {
            this.tmdbMoviesIntegrations = tmdbMoviesIntegrations;
        }
        public async Task<Movie?> GetMovieByIdAsync(int movieId)
        {
            TmdbMovie? tmdbMovie = await tmdbMoviesIntegrations.GetMovieAsync(movieId);

            
        }

        public Task<List<Movie>> GetMoviesByIdAsync(List<int> movieIds)
        {
            throw new NotImplementedException();
        }

        public Movie TmdbMovieToMovie(Movie movie, TmdbMovie tmdbMovie)
        {
            if (tmdbMovie.Id == movie.Id)
            {
                movie.ImageURL = tmdbMovie.ImageURL;
                movie.Synopsis = tmdbMovie.Synopsis;
            }
            return movie;
        }
    }
}
