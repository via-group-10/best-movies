namespace BestMovies.Api.Models
{
    public class UserFavoriteMovie
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public BestMoviesUser User { get; set; }
        public Movie Movie { get; set; }
    }
}
