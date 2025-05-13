using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace User.Management.API.AppDbContext
{
    public class UserManagementDbContext : IdentityDbContext<IdentityUser>
    {
        public UserManagementDbContext(DbContextOptions<UserManagementDbContext> Options) : base(Options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedingRoles(builder);
        }

        private static void SeedingRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() {Id = "Role.Admin",Name = "Admin",ConcurrencyStamp="1",NormalizedName="Administrator"},
                 new IdentityRole() { Id = "Role.User",Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" },
                  new IdentityRole() { Id = "Role.HR", Name = "HR", ConcurrencyStamp = "3", NormalizedName = "Human Resource" }
                );
        }
    }
}
