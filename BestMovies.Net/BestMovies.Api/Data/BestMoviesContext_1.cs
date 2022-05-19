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
                entity.HasKey(d => d.Id);

                entity.ToTable("directors");

                entity.Property(e => e.MovieId)
                    .HasColumnName("movie_id");

                entity.Property(e => e.PersonId)
                    .HasColumnName("person_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.HasOne(d => d.Movie)
                    .WithMany(m => m.Directors)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Person)
                    .WithMany()
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("movies");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Title)
                    .HasColumnName("title");

                entity.Property(e => e.Year)
                    .HasColumnName("year");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("people");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Birth)
                    .HasColumnName("birth");

                entity.Property(e => e.Name)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasKey(d => d.MovieId);

                entity.ToTable("ratings");

                entity.Property(e => e.MovieId)
                    .HasColumnName("movie_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Value)
                    .HasColumnName("rating");

                entity.Property(e => e.Votes)
                    .HasColumnName("votes");

                entity.HasOne(d => d.Movie)
                    .WithMany(m => m.Ratings)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Star>(entity =>
            {
                entity.HasKey(d => d.Id);

                entity.ToTable("stars");

                entity.Property(e => e.MovieId)
                    .HasColumnName("movie_id");

                entity.Property(e => e.PersonId)
                    .HasColumnName("person_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.HasOne(d => d.Movie)
                    .WithMany(m => m.Stars)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Person)
                    .WithMany()
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
