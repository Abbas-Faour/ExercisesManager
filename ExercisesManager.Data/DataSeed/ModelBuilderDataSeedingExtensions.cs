using ExercisesManager.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExercisesManager.Data.DataSeed
{
    internal static class ModelBuilderDataSeedingExtensions
    {
        internal static void SeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exercise>().HasData(new Exercise
            {
                Id = 1,
                Name = "Exercise 1",
                Description = "Exercise 1 Description",
                Duration = new TimeSpan(0, 10, 0),
                CreatedAt = DateTime.Now,
                CreatedBy = "Data-Seed",
            });

            modelBuilder.Entity<Exercise>().HasData(new Exercise
            {
                Id = 2,
                Name = "Exercise 2",
                Description = "Exercise 2 Description",
                Duration = new TimeSpan(0, 10, 0),
                CreatedAt = DateTime.Now,
                CreatedBy = "Data-Seed",
            });
        }
    }
}