using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WeatherApp.API.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

       // public DbSet<IdentityUser> NewUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = 1 + "",
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Id = 2 + "",
                Name = "User",
                NormalizedName = "USER"
            },
            new IdentityRole
            {
                Id = 3 + "",
                Name = "Tester",
                NormalizedName = "TESTER"
            }
            );

            base.OnModelCreating(builder);
        }

    }
}
