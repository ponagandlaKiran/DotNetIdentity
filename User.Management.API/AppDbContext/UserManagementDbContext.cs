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
        }
    }
}
