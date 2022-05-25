using BestMovies.Api.Models;
using BestMovies.Api.Models.Filters;
using BestMovies.Api.Repository.Abstractions;
using BestMovies.Api.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestMovies.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AuthenticationController : BestMoviesControllerBase
{
    private readonly AuthenticationService authService;

    public AuthenticationController(AuthenticationService authService)
    {
        this.authService = authService;
    }

    /// <summary>
    /// </summary>
    /// <param name="username">Zatial akekolvek meno</param>
    /// <param name="passHash">Taketo - 04e8c7551b02ec45ab665ed3040b9ae400342118f6225c68a13fdd0971667e2f</param>
    /// <returns></returns>
    [HttpPost("signin")]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn([FromBody] UserDTO user)
    {
        var pouzivatel = await authService.SigninUser(user.Username, user.Password);

        if (pouzivatel == null)
            return Unauthorized(new { message = "Incorrect username, or password." });

        // 2020-12-08: Heslo odosielane pre FE, aby mohlo byt pouzite pri zmene hesla na overenie stareho
        pouzivatel.Pass = null!;

        return Ok(pouzivatel);
    }

    [HttpPost("signup")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp([FromBody] UserDTO user)
    {
        var success = await authService.SignupUser(user.Username, user.Password);

        if (!success)
            return Unauthorized(new { message = "You were not able to sign up" });

        return Ok(new { message = "You are successfully signed up." });
    }
}
