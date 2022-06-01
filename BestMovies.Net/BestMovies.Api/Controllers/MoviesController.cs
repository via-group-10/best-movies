using BestMovies.Api.Exceptions;
using BestMovies.Api.Middleware;
using BestMovies.Api.Models;
using BestMovies.Api.Models.Filters;
using BestMovies.Api.Models.RequestDTO;
using BestMovies.Api.Repository.Abstractions;
using BestMovies.Api.Service;
using BestMovies.Api.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestMovies.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MoviesController : BestMoviesControllerBase
{
    private readonly IMovieService movieService;


    public MoviesController(
        IMovieService movieService,
        AuthenticationService authService) : base(authService)
    {
        this.movieService = movieService;
    }

    // GET api/movies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> GetList([FromQuery] MovieFilter? filter)
    {
        if (filter == null)
        {
            filter = new MovieFilter();
        }
        var result = await movieService.GetMoviesByFilterAsync(filter);
        return Ok(result);
    }

    //GET api/movies/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> Get(int id)
    {
        var result = await movieService.GetMovieByIdAsync(id);

        return result != null ? Ok(result) : NotFound(new { message = $"Movie with the id {id} was not found." });
    }

    [HttpPost("{movieId}/comments")]
    public async Task<ActionResult> PostComment([FromRoute] int movieId, [FromBody] AddCommentDTO commentRequest)
    {
        string? username = HttpContext.User.Identity?.Name;

        if (username == null)
        {
            return BadRequest(new { message = "A co si ty ked nemas meno, hmm!?" });
        }

        Comment comment = new Comment()
        {
            Username = username,
            Text = commentRequest.Text
        };
        var result = await movieService.AddCommentToMovieAsync(movieId, comment);
        return result ? Ok(new { message = "Comment was successfully posted." }) : BadRequest(new { message = "Comment was not posted." });
    }

    [HttpPut("myfavorite/{movieId}")]
    public async Task<ActionResult> MakeMovieMyFavorite([FromRoute] int movieId)
    {
        try
        {
            var username = HttpContext.User.Identity?.Name;

            if (username == null)
            {
                return BadRequest(new { message = "A co si ty ked nemas meno, hmm!?" });
            }

            bool result = await movieService.AddFavoriteMovieToUserAsync(username, movieId);
            return result ? Ok(new { message = "Movie was successfully added to your favorites." }) : BadRequest(new { message = "Movie was not added to you favorites." });
        }
        catch (RecordAlreadyExistsException ex)
        {
            return BadRequest(new { message = "Movie was already added to your favorites." });
        }

    }

    [HttpGet("myfavorite")]
    public async Task<ActionResult> MyFavoriteMovies()
    {
        var username = HttpContext.User.Identity?.Name;

        if (username == null)
        {
            return BadRequest(new { message = "A co si ty ked nemas meno, hmm!?" });
        }

        List<Movie> result = await movieService.GetMyFavoriteListAsync(username);
        return Ok(result);
    }

    [HttpGet("favorite")]
    public async Task<ActionResult> FavoriteMovies()
    {
        List<Movie> result = await movieService.GetFavoriteListAsync();
        return Ok(result);
    }
}
