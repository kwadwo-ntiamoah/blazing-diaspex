using Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Category>()
                .HasIndex(c => new { c.Type, c.Title })
                .IsUnique();

            base.OnModelCreating(builder);
        }

        public DbSet<Profile>? Profiles {get; set;}
        public DbSet<Category>? Categories { get; set; }
        public DbSet<News>? News { get; set; }
        public DbSet<Post>? Posts { get; set; }
        public DbSet<Reply>? Replies { get; set; }
    }
}