using BestMovies.Api.Models;
using BestMovies.Api.Repository.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace BestMovies.Api.Service
{
    public class AuthenticationService
    {
        private readonly IConfiguration configuration;
        private readonly IUserRepository userRepository;

        public AuthenticationService(IUserRepository userRepository, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.userRepository = userRepository;
        }

        public bool? AuthenticateRequest(HttpContext context)
        {
            try
            {
                var token = context.Request.Headers["Authentication"].FirstOrDefault()?.Split(" ").Last();

                if (string.IsNullOrEmpty(token))
                    return null;

                new JwtSecurityTokenHandler().ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetValue<string>("JwtKey"))),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                //context.SignInAsync(new ClaimsPrincipal(ClaimsToUser(jwtToken.Claims)));
                // attach user to context on successful jwt validation
                var bmUser = ClaimsToUser(jwtToken.Claims);

                // TODO - dotiahnutie detailu z DB
                // bmUser = apiManager.GetPouzivatelByEmail(bmUser.LoginName);

                // context.User = new ClaimsPrincipal(bmUser);
                context.User = new GenericPrincipal(bmUser, new[] { bmUser.Rola.ToString() });
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> SignupUser(string username, string password)
        {
            BestMoviesUser? user = await userRepository.RegisterUserAsync(new BestMoviesUser() { Username = username, Pass = password });

            return user != null;
        }


        // TODO: REFRESH TOKEN !!!
        public async Task<BestMoviesUser?> SigninUser(string username, string password)
        {
            BestMoviesUser? user = await userRepository.FindUserMatchAsync(username, password);

            //var hashedKed = new HashedKey(SHA256.Create(), "Heslo");
            //var user = new BestMoviesUser { LoginName = "FakeUser", Pass = hashedKed.KeyHexString };

            //Authenticate User, Check if it’s a registered user in Database 
            if (user == null)
                return null;

            if (password == user.Pass)
            {
                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetValue<string>("JwtKey")));

                //Authentication successful, Issue Token with user credentials 
                //Generate Token for user 
                var JWToken = new JwtSecurityToken(
                    //issuer: "http://localhost:45092/",
                    //audience: "http://localhost:45092/",
                    claims: UserToClaims(user),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(
                        DateTime.Now.AddMinutes(60)).DateTime,
                    //Using HS256 Algorithm to encrypt Token  
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
                );

                user.AuthToken = "Bearer " + new JwtSecurityTokenHandler().WriteToken(JWToken);
                // var t = new JwtSecurityTokenHandler().ReadJwtToken(token);
                CurrentUser = user;
                return user;
            }

            return null;
        }

        public BestMoviesUser? CurrentUser { get; private set; }

        //Using hard coded collection list as Data Store for demo. 
        //In reality, User data comes from Database or other Data Source 
        private IEnumerable<Claim> UserToClaims(BestMoviesUser user)
        {
            IEnumerable<Claim> claims = new[]
            {
            new(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Role, user.Rola.ToString())
        };

            return claims;
        }



        private BestMoviesUser ClaimsToUser(IEnumerable<Claim> claims)
        {
            return new BestMoviesUser
            {
                Username = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value,
                Rola = Enum.Parse<BestMoviesRole>(claims.First(c => c.Type == ClaimTypes.Role).Value)
            };
        }
    }
}
