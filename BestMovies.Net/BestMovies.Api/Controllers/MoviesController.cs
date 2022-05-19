using BestMovies.Api.Models;
using BestMovies.Api.Models.Filters;
using BestMovies.Api.Repository.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BestMovies.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly string? userId;
    private readonly IMovieRepository movieRepository;
    public MoviesController(
        IHttpContextAccessor httpContextAccessor,
        IMovieRepository movieRepository)
    {
        // userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        this.movieRepository = movieRepository;
    }

    [HttpGet(Name = "GetMovies")]
    public async Task<ActionResult<IEnumerable<Movie>>> Get([FromQuery] MovieFilter? filter)
    {
        if (filter == null)
        {
            filter = new MovieFilter();
        }
        var result = await movieRepository.GetMoviesAsync(filter);
        return Ok(result);
    }
}
