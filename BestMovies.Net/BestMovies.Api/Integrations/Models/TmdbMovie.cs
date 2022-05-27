using BestMovies.Api.Models;
using Newtonsoft.Json;

namespace BestMovies.Api.Integrations.Models
{
    public class TmdbMovie
    {
        public int Id { get; set; }

        [JsonProperty("overview")]
        public string? Synopsis { get; set; }

        [JsonProperty("poster_path")]
        public string? ImageUrl {get; set;}

        [JsonProperty("release_date")]
        public DateTime? releaseDate { get; set; }
    }
}
