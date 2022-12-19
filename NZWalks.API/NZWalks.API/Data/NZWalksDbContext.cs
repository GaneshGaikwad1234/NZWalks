using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext:DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options):base(options)
        {

        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Region>().HasNoKey();
        //    modelBuilder.Entity<Walk>().HasNoKey();
        //    modelBuilder.Entity<WalkDifficulty>().HasNoKey();
        //}
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }
    }
}
