using BestMovies.Api.Middleware;
using BestMovies.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace BestMovies.Api.Controllers;

public abstract class BestMoviesControllerBase : ControllerBase
{
    protected AuthenticationService authService;

    public BestMoviesControllerBase(AuthenticationService authService)
    {
        this.authService = authService;
    }
}
