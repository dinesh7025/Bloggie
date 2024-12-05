using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Bloggie.web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Define role IDs
            var superAdminRoleId = Guid.NewGuid().ToString();
            var adminRoleId = Guid.NewGuid().ToString();
            var userRoleId = Guid.NewGuid().ToString();

            // Seed Roles
            var roles = new List<IdentityRole>
    {
        new IdentityRole
        {
            Id = superAdminRoleId,
            Name = "SuperAdmin",
            NormalizedName = "SUPERADMIN",
            ConcurrencyStamp = Guid.NewGuid().ToString()
        },
        new IdentityRole
        {
            Id = adminRoleId,
            Name = "Admin",
            NormalizedName = "ADMIN",
            ConcurrencyStamp = Guid.NewGuid().ToString()
        },
        new IdentityRole
        {
            Id = userRoleId,
            Name = "User",
            NormalizedName = "USER",
            ConcurrencyStamp = Guid.NewGuid().ToString()
        }
    };

            builder.Entity<IdentityRole>().HasData(roles);

            // Seed SuperAdmin User
            var superAdminId = Guid.NewGuid().ToString();
            var superAdminUser = new IdentityUser
            {
                Id = superAdminId,
                UserName = "superAdmin1",
                NormalizedUserName = "SUPERADMIN1",
                Email = "superAdmin1@bloggie.com",
                NormalizedEmail = "SUPERADMIN1@BLOGGIE.COM",
                EmailConfirmed = true
            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "superAdmin@1");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            // Assign all roles to SuperAdmin
            var superAdminRoles = new List<IdentityUserRole<string>>
    {
        new IdentityUserRole<string> { RoleId = superAdminRoleId, UserId = superAdminId },
        new IdentityUserRole<string> { RoleId = adminRoleId, UserId = superAdminId },
        new IdentityUserRole<string> { RoleId = userRoleId, UserId = superAdminId }
    };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }


    }

}
