using ExercisesManager.Data.DataSeed;
using ExercisesManager.Data.Entites;
using ExercisesManager.Data.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExercisesManager.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, long,
        IdentityUserClaim<long>, ApplicationUserRole, IdentityUserLogin<long>, IdentityRoleClaim<long>, IdentityUserToken<long>>
    {
        public DbSet<Exercise> Exercises { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Application User
            modelBuilder.Entity<ApplicationUser>().Property(x => x.FirstName).IsRequired();
            modelBuilder.Entity<ApplicationUser>().Property(x => x.LastName).IsRequired();
            modelBuilder.Entity<ApplicationUser>().Property(x => x.DateOfBirth).IsRequired().HasColumnType("timestamp without time zone");

            // Exercise
            modelBuilder.Entity<Exercise>().Property(x => x.Name).IsRequired().HasMaxLength(225);
            modelBuilder.Entity<Exercise>().Property(x => x.Description).IsRequired();
            modelBuilder.Entity<Exercise>().Property(x => x.Duration).IsRequired().HasColumnType("time without time zone");

            //User Exercises
            modelBuilder.Entity<UserExercises>(UserExercise =>
            {
                UserExercise.HasKey(ue => new { ue.ApplicationUserId, ue.ExerciseId });

                UserExercise.HasOne(ue => ue.ApplicationUser)
                    .WithMany(u => u.UserExercises)
                    .HasForeignKey(ue => ue.ApplicationUserId)
                    .IsRequired();

                UserExercise.HasOne(ue => ue.Exercise)
                    .WithMany(e => e.UserExercises)
                    .HasForeignKey(ue => ue.ExerciseId)
                    .IsRequired();
            });
            
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

            modelBuilder.SeedData();
        }
    }
}