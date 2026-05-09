using HRPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace HRPlatform.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Skill> Skills { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>().HasIndex(c => c.Email).IsUnique();
            modelBuilder.Entity<Skill>().HasIndex(skill => skill.Name).IsUnique();
        }

    }
}
