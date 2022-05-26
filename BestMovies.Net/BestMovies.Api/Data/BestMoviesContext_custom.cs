using BestMovies.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BestMovies.Api.Data
{
    public partial class BestMoviesContext
    {
        public virtual DbSet<BestMoviesUser> Users { get; set; } = null!;

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BestMoviesUser>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("users");

                entity.Property(e => e.Name)
                    .HasColumnName("login_name")
                    .IsRequired();

                entity.Property(e => e.Pass)
                    .HasColumnName("pass")
                    .IsRequired();

                entity.Property(e => e.Rola)
                    .HasColumnName("rola");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityColumn();

                entity.Ignore(e => e.IsAuthenticated);
                entity.Ignore(e => e.AuthenticationType);
                entity.Ignore(e => e.AuthToken);
            });

            modelBuilder.Entity<UserFavoriteMovie>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("users_favorite_movies");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.MovieId)
                    .HasColumnName("movie_id")
                    .ValueGeneratedNever();

                entity.HasOne(e => e.User)
                    .WithMany(e => e.FavoriteMovies)
                    .HasForeignKey(e => e.UserId);

                entity.HasOne(e => e.Movie)
                    .WithMany(e => e.FavoredByUsers)
                    .HasForeignKey(e => e.MovieId);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("comments");

                entity.Property(e => e.Username)
                    .HasColumnName("username");

                entity.Property(e => e.Text)
                    .HasColumnName("text")
                    .HasColumnType("TEXT");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasDefaultValueSql("getdate()");

                entity.HasOne<Movie>()
                    .WithMany(e => e.Comments)
                    .HasForeignKey(e => e.MovieId);
            });
        }
    }
}
