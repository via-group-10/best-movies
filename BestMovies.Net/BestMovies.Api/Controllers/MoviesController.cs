using BestMovies.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace BestMovies.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{

    [HttpGet(Name = "GetMovies")]
    public async Task<ActionResult<IEnumerable<Movie>>> Get()
    {
        var res = await Task.FromResult(Enumerable.Range(1, 5).Select(x => new Movie
        {
            Description = $"{x}"
        }));
        return Ok(res);
    }
}
