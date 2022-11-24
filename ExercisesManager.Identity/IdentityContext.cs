using ExercisesManager.Data.Entities;
using ExercisesManager.Identity.DataSeed;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExercisesManager.Identity
{
    public class IdentityContext : IdentityDbContext<ApplicationUser, ApplicationRole, long,
        IdentityUserClaim<long>, ApplicationUserRole, IdentityUserLogin<long>, IdentityRoleClaim<long>, IdentityUserToken<long>>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Application User
            modelBuilder.Entity<ApplicationUser>().Property(x => x.FirstName).IsRequired().HasMaxLength(225);
            modelBuilder.Entity<ApplicationUser>().Property(x => x.LastName).IsRequired().HasMaxLength(225);
            modelBuilder.Entity<ApplicationUser>().Property(x => x.CreatedBy).IsRequired().HasMaxLength(225);
            modelBuilder.Entity<ApplicationUser>().Property(x => x.CreatedAt).IsRequired().HasColumnType("timestamp without time zone");
            modelBuilder.Entity<ApplicationUser>().Property(x => x.DateOfBirth).IsRequired().HasColumnType("timestamp without time zone");
            modelBuilder.Entity<ApplicationUser>().Property(x => x.IsDeleted).HasDefaultValue(false);

            // Application User Role
            modelBuilder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Seed();
        }
    }
}