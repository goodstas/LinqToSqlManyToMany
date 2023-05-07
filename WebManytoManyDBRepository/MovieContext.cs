using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using WebManytoManyDBRepository.Entities;

namespace WebManytoManyDBRepository
{   
    public class MovieContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public MovieContext(DbContextOptions options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(_ =>
                _.HasKey(_ => _.Id)
            ); 

            modelBuilder.Entity<Genre>(_ =>
               _.HasKey(_ => _.Id)
            ); 

            modelBuilder.Entity<Movie>().HasIndex(e => e.Id).HasDatabaseName("MovieId_Index");
            modelBuilder.Entity<Genre>().HasIndex(e => e.Id).HasDatabaseName("GenreId_Index");

            modelBuilder.Entity<Movie>()
                        .HasMany(_ => _.Genres)
                        .WithMany(_ => _.Movies);
        }
    }
}
