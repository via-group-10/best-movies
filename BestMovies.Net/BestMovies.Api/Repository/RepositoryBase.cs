using BestMovies.Api.Data;

namespace BestMovies.Api.Repository;

public class RepositoryBase
{
    protected readonly BestMoviesContext Context;

    public RepositoryBase(BestMoviesContext BestMoviesContext)
    {
        this.Context = BestMoviesContext;
    }
}
