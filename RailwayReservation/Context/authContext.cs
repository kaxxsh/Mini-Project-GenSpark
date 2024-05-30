using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace project.Context
{
    public class authContext : IdentityDbContext
    {
        public authContext(DbContextOptions<authContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var AdminId = "a71a55d6-99d7-4123-b4e0-1218ecb90e3e";
            var UserId = "c309fa92-2123-47be-b397-a1c77adb502c";
            var SuperAdminId = "6AD15F9F-0580-4DAA-8FFA-1E25DC0FB381";


            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = AdminId,
                    ConcurrencyStamp = AdminId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                },
                new IdentityRole
                {
                    Id = UserId,
                    ConcurrencyStamp = UserId,
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                },
                new IdentityRole
                {
                    Id = SuperAdminId,
                    ConcurrencyStamp = SuperAdminId,
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
