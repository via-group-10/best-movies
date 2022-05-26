namespace BestMovies.Api.AppExtensions
{
    public static class ConfigurationExtensions
    {
        public static string GetJwtKey(this IConfiguration configuration)
        {
            return configuration.GetValue<string>("JwtKey");
        }
    }
}
