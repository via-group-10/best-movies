using System.Security.Principal;

namespace BestMovies.Api.Models
{
    public class BestMoviesUser : IIdentity
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Pass { get; set; } = string.Empty;
        public string AuthToken { get; set; } = string.Empty;

        public override string ToString()
        {
            return Name;
        }

        #region Profile

        public List<UserFavoriteMovie> FavoriteMovies { get; set; } = new List<UserFavoriteMovie>();

        #endregion

        #region IIdentity

        public string AuthenticationType => "Bearer";

        public bool IsAuthenticated => true;

        public string Name => Username;

        public BestMoviesRole Rola { get; set; } = BestMoviesRole.User;

        #endregion
    }
}
