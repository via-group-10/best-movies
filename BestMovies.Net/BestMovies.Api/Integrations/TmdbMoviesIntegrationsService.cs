using BestMovies.Api.Integrations.Models;
using BestMovies.Api.Models;

namespace BestMovies.Api.Integrations
{
    public class TmdbMoviesIntegrationsService
    {
        HttpClient client;

        public Movie GetMovies(List<int> movieIds)
        {
            TmdbMovie response = new TmdbMovie(); // body from response

            return response.ToMovie();
        }
    }
}
