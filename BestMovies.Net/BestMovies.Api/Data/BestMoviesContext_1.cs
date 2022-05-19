using BestMovies.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BestMovies.Api.Data
{
    public partial class BestMoviesContext : DbContext
    {
        public BestMoviesContext()
        {
        }

        public BestMoviesContext(DbContextOptions<BestMoviesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Director> Directors { get; set; } = null!;
        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<Rating> Ratings { get; set; } = null!;
        public virtual DbSet<Star> Stars { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Director>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("directors");

                entity.Property(e => e.MovieId)
                    .HasColumnName("movie_id");

                entity.Property(e => e.PersonId)
                    .HasColumnName("person_id");

                entity.HasOne(d => d.Movie)
                    .WithMany()
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Person)
                    .WithMany()
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("movies");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasColumnName("title");

                entity.Property(e => e.Year)
                    .HasColumnName("year");

                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("people");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Birth)
                    .HasColumnName("birth");

                entity.Property(e => e.Name)
                    .HasColumnName("name");

                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ratings");

                entity.Property(e => e.MovieId)
                    .HasColumnName("movie_id");

                entity.Property(e => e.Value)
                    .HasColumnName("rating");

                entity.Property(e => e.Votes)
                    .HasColumnName("votes");

                entity.HasOne(d => d.Movie)
                    .WithMany()
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Star>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("stars");

                entity.Property(e => e.MovieId)
                    .HasColumnName("movie_id");

                entity.Property(e => e.PersonId)
                    .HasColumnName("person_id");

                entity.HasOne(d => d.Movie)
                    .WithMany()
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Person)
                    .WithMany()
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
