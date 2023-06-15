using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie_Producer>()
            .HasOne(m => m.Movie)
            .WithMany(m => m.Movie_Producers)
            .HasForeignKey(mi => mi.MovieId);

        modelBuilder.Entity<Movie_Producer>()
            .HasOne(m => m.Producer)
            .WithMany(m => m.Movie_Producers)
            .HasForeignKey(mi => mi.ProducerId);

        modelBuilder.Entity<Producer>()
            .HasOne(p => p.Studio)
            .WithMany(s => s.Producers)
            .HasForeignKey(p => p.StudioId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    public DbSet<Producer> Producers { get; set; }

    public DbSet<Movie> Movie { get; set; }

    public DbSet<Cinema> Cinemas { get; set; }

    public DbSet<Studio> Studios { get; set; }

    public DbSet<Employees> Employees { get; set; }

    public DbSet<Movie_Producer> Movie_Producers { get; set; }

}   