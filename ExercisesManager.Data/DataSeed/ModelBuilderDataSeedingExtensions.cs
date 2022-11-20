using ExercisesManager.API.Constants;
using ExercisesManager.Data.Entites;
using ExercisesManager.Data.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExercisesManager.Data.DataSeed
{
    internal static class ModelBuilderDataSeedingExtensions
    {
        internal static void SeedData(this ModelBuilder modelBuilder)
        {
            var AdminRoleID = 1;
            modelBuilder.Entity<ApplicationRole>().HasData(new ApplicationRole { Id = AdminRoleID, Name = Roles.Admin });

            var AdminUserID = 1;
            var hasher = new PasswordHasher<ApplicationUser>();

            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = AdminUserID,
                FirstName = "Abbas",
                LastName = "Faour",
                DateOfBirth = new DateTime(2000,05,26),
                Email = "Abbasfaour25@gmail.com",
                UserName = "abbasfaour",
                PasswordHash = hasher.HashPassword(null,"Pa$$w0rd"),
                PhoneNumber = "71435810",
                PhoneNumberConfirmed = true
            });

            modelBuilder.Entity<ApplicationUserRole>().HasData(new ApplicationUserRole { UserId = AdminUserID, RoleId = AdminRoleID });
        }
    }
}