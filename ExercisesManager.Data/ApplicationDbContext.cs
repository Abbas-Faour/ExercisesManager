using ExercisesManager.Data.DataSeed;
using ExercisesManager.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExercisesManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Exercise> Exercises { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Exercise
            modelBuilder.Entity<Exercise>().Property(x => x.Name).IsRequired().HasMaxLength(225);
            modelBuilder.Entity<Exercise>().Property(x => x.Description).IsRequired();
            modelBuilder.Entity<Exercise>().Property(x => x.Duration).IsRequired().HasColumnType("time without time zone");

            //User Exercise
            modelBuilder.Entity<UserExercise>().Ignore(x => x.Id);
            modelBuilder.Entity<UserExercise>().HasKey(x => new { x.ApplicationUserId, x.ExerciseId });
            modelBuilder.Entity<UserExercise>().Property(x => x.IsDeleted).HasDefaultValue(false);

            modelBuilder.SeedData();
        }
    }
}