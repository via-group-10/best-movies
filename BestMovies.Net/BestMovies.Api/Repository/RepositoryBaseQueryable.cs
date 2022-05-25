using BestMovies.Api.Data;

namespace BestMovies.Api.Repository
{
    public class RepositoryBaseQueryable<T> : RepositoryBase where T : class
    {
        protected IQueryable<T> baseQuery;

        public RepositoryBaseQueryable(BestMoviesContext BestMoviesContext) : base(BestMoviesContext)
        {
            baseQuery = this.Context.Set<T>().AsQueryable();
        }
    }
}
