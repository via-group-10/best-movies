using BestMovies.Api.Data;

namespace BestMovies.Api.Repository;

public class RepositoryBase<T> where T: class
{
    protected readonly BestMoviesContext Context;
    protected IQueryable<T> baseQuery;

    public RepositoryBase(BestMoviesContext BestMoviesContext)
    {
        this.Context = BestMoviesContext;
        baseQuery = this.Context.Set<T>().AsQueryable();
    }
}
