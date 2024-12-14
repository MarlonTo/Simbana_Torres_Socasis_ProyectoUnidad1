using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUser>().HasKey(m => m.Id);
            builder.Entity<IdentityUser>()
                 .Property(u => u.Id)
                 .ValueGeneratedOnAdd();

            builder.Entity<IdentityRole>().HasKey(m => m.Id);
            builder.Entity<IdentityRole>()
                .Property(r => r.Id)
                .ValueGeneratedOnAdd();



        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
