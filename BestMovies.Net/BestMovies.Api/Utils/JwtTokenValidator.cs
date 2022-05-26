using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BestMovies.Api.Utils
{
    public class JwtTokenValidator
    {
        public class Parameters : TokenValidationParameters
        {
            public Parameters(string key)
            {
                ValidateIssuerSigningKey = true;
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                ValidateIssuer = false;
                ValidateAudience = false;
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero;
            }
        }
    }
}
