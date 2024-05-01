using BeerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BeerApi.Data
{
    public class BeerContext : DbContext
    {
        public BeerContext(DbContextOptions<BeerContext> options) : base(options)
        {
        }

        public DbSet<Beer> Beers { get; set; }
        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Bar> Bars { get; set; }
        public DbSet<BreweryBeer> BreweryBeers { get; set; }
        public DbSet<BarBeer> BarBeers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BreweryBeer>()
                .HasKey(bb => new { bb.BreweryId, bb.BeerId });

            modelBuilder.Entity<BarBeer>()
                .HasKey(bb => new { bb.BarId, bb.BeerId });

            modelBuilder.Entity<Beer>()
                .HasOne(b => b.Brewery)
                .WithMany()
                .HasForeignKey(b => b.BreweryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Beer>()
                .HasMany(b => b.BarBeers)
                .WithOne(bb => bb.Beer)
                .HasForeignKey(bb => bb.BeerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Brewery>()
                .HasMany(b => b.BreweryBeers)
                .WithOne(bb => bb.Brewery)
                .HasForeignKey(bb => bb.BreweryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Bar>()
                .HasMany(b => b.BarBeers)
                .WithOne(bb => bb.Bar)
                .HasForeignKey(bb => bb.BarId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
