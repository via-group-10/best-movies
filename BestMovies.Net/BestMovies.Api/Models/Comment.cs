namespace BestMovies.Api.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }

        public int MovieId { get; set; }
    }
}