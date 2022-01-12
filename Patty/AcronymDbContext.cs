using Microsoft.EntityFrameworkCore;
using Patty.DBModels;
using System;

namespace Patty
{
    internal class AcronymDbContext : DbContext
    {
        public DbSet<Acronym>? Acronyms { get; set; }
        public DbSet<Tag>? Tags { get; set; }
        public DbSet<AcronymTag>? AcronymTags { get; set; }


        private readonly string? databasePath;

        public AcronymDbContext(string dbPath)
        {
            databasePath = dbPath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={databasePath ?? throw new ArgumentNullException(nameof(databasePath))}");
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcronymTag>().HasKey(at => new { at.AcronymId, at.TagId });
            modelBuilder.Entity<AcronymTag>().HasOne(at => at.Acronym).WithMany(t => t.AcronymTags).HasForeignKey(at => at.AcronymId);
            modelBuilder.Entity<AcronymTag>().HasOne(at => at.Tag).WithMany(t => t.AcronymTags).HasForeignKey(at => at.TagId);
        }
    }
}
