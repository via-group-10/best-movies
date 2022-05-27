using BestMovies.Api.Middleware;
using BestMovies.Api.Models;
using BestMovies.Api.Models.Filters;
using BestMovies.Api.Models.RequestDTO;
using BestMovies.Api.Repository.Abstractions;
using BestMovies.Api.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BestMovies.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MoviesController : BestMoviesControllerBase
{
    private readonly IMovieRepository movieRepository;


    public MoviesController(
        IMovieRepository movieRepository, 
        AuthenticationService authService) : base(authService)
    {
        this.movieRepository = movieRepository;
    }

    // GET api/movies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> GetList([FromQuery] MovieFilter? filter)
    {
        if (filter == null)
        {
            filter = new MovieFilter();
        }
        var result = await movieRepository.GetMoviesAsync(filter);
        return Ok(result);
    }

    //GET api/movies/{id}
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<Movie>> Get(int id)
    {
        var result = await movieRepository.GetMovieByIdAsync(id);

        return result != null ? Ok(result) : NotFound(new { message = $"Movie with the id {id} was not found." });
    }

    [HttpPost("{movieId}/comments")]
    public async Task<ActionResult> PostComment([FromRoute]int movieId, [FromBody] AddCommentDTO commentRequest)
    {
        string? username = HttpContext.User.Identity?.Name;

        if (username == null)
        {
            return BadRequest(new { message = "A co si picus ked nemas meno, kokotko!?" });
        }

        Comment comment = new Comment()
        {
            Username = username,
            Text = commentRequest.Text
        };
        var result = await movieRepository.AddCommentToMovieAsync(movieId, comment);
        return result ? Ok(new { message = "Comment was successfully posted." }) : BadRequest(new { message = "Comment was not posted." });
    }
}
