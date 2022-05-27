using BestMovies.Api.Integrations.Models;
using BestMovies.Api.Models;
using BestMovies.Api.AppExtensions;
using System.Text.Json;
using BestMovies.Api.Integrations.Abstractions;

namespace BestMovies.Api.Integrations
{
    public class TmdbMoviesIntegrationsService : ITmdbMoviesIntegrations
    {
        private HttpClient client;
        private readonly IConfiguration configuration;
        private readonly string path;

        public TmdbMoviesIntegrationsService(IConfiguration configuration, HttpClient client)
        {
            this.configuration = configuration;
            this.client = client;
            path = @"https://api.themoviedb.org/3/find/tt00";
        }

        public async Task<TmdbMovie?> GetMovieAsync(int movieId)
        {
            var response = await client.GetAsync(path + movieId + configuration.GetTmdbApiKey());
            if(response.IsSuccessStatusCode)
            {
                var stringResult = await response.Content.ReadAsStringAsync();
                if (stringResult == null)
                {
                    return null;
                }
                TmdbMovie? result = JsonSerializer.Deserialize<TmdbMovie>(stringResult!);
                result!.Id = movieId;
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<TmdbMovie?>> GetMoviesAsync(List<int> movieIds)
        {
            List<Task<string>> tasks = new List<Task<string>>();
            foreach(var movieId in movieIds)
            {
                async Task<string> GetMovieAsync()
                {
                    var response = await client.GetAsync(path + movieId + configuration.GetTmdbApiKey());
                    return await response.Content.ReadAsStringAsync();
                }
                tasks.Add(GetMovieAsync());
            }

            await Task.WhenAll(tasks);

            var movieResponses = new List<string>();

            foreach(var task in tasks)
            {
                var movieResponse = await task;
                movieResponses.Add(movieResponse);
            }

            HttpResponseMessage response = await client.GetAsync(path);
            TmdbMovie response = new TmdbMovie(); // body from response

            return response.ToMovie();
        }
    }
}
