using BestMovies.Api.Models;
using System.Text.Json.Serialization;

namespace BestMovies.Api.Integrations.Models
{
    public class TmdbMovie
    {
        public int Id { get; set; }

        [JsonPropertyName("overview")]
        public string? Synopsis { get; set; }

        [JsonPropertyName("poster_path")]
        public string? ImageURL {get; set;}
    }
}
