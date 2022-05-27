namespace BestMovies.Api.AppExtensions
{
    public static class ConfigurationExtensions
    {
        public static string GetJwtKey(this IConfiguration configuration)
        {
            return configuration.GetValue<string>("JwtKey");
        }
        public static string GetTmdbApiKey(this IConfiguration configuration)
        {
            return configuration.GetValue<string>("TmdbKey");
        }
        public static string GetImageUrlPath(this IConfiguration configuration)
        {
            return configuration.GetValue<string>("ImageUrlPath");
        }
    }
}
