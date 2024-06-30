using AluraMovieApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AluraMovieApi.Data;

public class MovieContext : DbContext
{

    public DbSet<Movie> Movies { get; set; }

    public DbSet<Theater> Theaters { get; set;}

    public DbSet<Adress> Adresses { get; set; }

    public DbSet<MovieSession> MovieSessions { get; set; }

    public MovieContext(DbContextOptions<MovieContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder){

        modelBuilder.Entity<Movie>()
                    .HasKey(m => m.Id); 

        modelBuilder.Entity<Adress>(entity =>{
                                        entity.HasKey(a => a.Id);
                                        entity.Property(a => a.Id).ValueGeneratedOnAdd();

                                        entity.HasOne(t => t.Theater)
                                              .WithOne(a => a.Adress)                                          
                                              .OnDelete(DeleteBehavior.Restrict);                                       
                                    });

        modelBuilder.Entity<Theater>(entity => {
                      
                          entity.HasKey(t => t.Id);

                          entity.Property(t => t.Id).ValueGeneratedOnAdd();
                      });

        modelBuilder.Entity<MovieSession>( entity => {
              entity.HasKey(s => new {s.MovieId, s.TheaterId });
              
              entity.HasOne(session => session.Movie)
                    .WithMany(movie => movie.MovieSessions)
                    .HasForeignKey(session => session.MovieId);

               entity.HasOne(session => session.Theater)
                     .WithMany(theater => theater.MovieSessions)
                     .HasForeignKey(session => session.TheaterId);
        });
    }    
}