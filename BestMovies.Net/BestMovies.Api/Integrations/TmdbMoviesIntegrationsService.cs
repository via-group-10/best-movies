using BestMovies.Api.Integrations.Models;
using BestMovies.Api.Models;
using BestMovies.Api.AppExtensions;
using System.Text.Json;
using BestMovies.Api.Integrations.Abstractions;
using Newtonsoft.Json.Linq;

namespace BestMovies.Api.Integrations
{
    public class TmdbMoviesIntegrationsService : ITmdbMoviesIntegrationsService
    {
        private HttpClient client;
        private readonly IConfiguration configuration;
        private readonly string path;
        private readonly string externalSource;

        public TmdbMoviesIntegrationsService(IConfiguration configuration, HttpClient client)
        {
            this.configuration = configuration;
            this.client = client;
            path = @"https://api.themoviedb.org/3/find/tt00";
            externalSource = @"&external_source=imdb_id";
        }

        public async Task<TmdbMovie?> GetMovieByIdAsync(int movieId)
        {
            var response = await client.GetAsync(path + movieId + configuration.GetTmdbApiKey() + externalSource);
            if(response.IsSuccessStatusCode)
            {
                var stringResult = await response.Content.ReadAsStringAsync();
                //Parse to JObject
                JObject jo = JObject.Parse(stringResult);
                //Select only the movie object from json
                JObject? movie = (JObject?) jo.SelectToken("movie_results[0]");
                if (movie == null)
                {
                    return null;
                }
                else
                {
                    TmdbMovie? result = movie!.ToObject<TmdbMovie>();
                    result!.Id = movieId;
                    return result;
                }
                //Cast JObject to TmdbMovie model
                
            }
            else
            {
                return null;
            }
        }

        public async Task<List<TmdbMovie?>> GetMoviesByIdAsync(List<int> movieIds)
        {
            List<Task<TmdbMovie?>> tasks = new List<Task<TmdbMovie?>>();
            foreach(var movieId in movieIds)
            {
                tasks.Add(GetMovieByIdAsync(movieId));
            }

            await Task.WhenAll(tasks);

            var responses = new List<TmdbMovie?>();

            foreach(var task in tasks)
            {
                var response = await task;
                responses.Add(response);
            }
            return responses;
        }
    }
}
