using ExercisesManager.Data.Entities;
using ExercisesManager.Identity.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExercisesManager.Identity.DataSeed
{
    public static class ModelBuilderDataSeedingExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var AdminRoleID = 1;
            var ClientRoleID = 2;
            modelBuilder.Entity<ApplicationRole>().HasData(new ApplicationRole { Id = AdminRoleID, Name = Roles.Admin, NormalizedName = Roles.Admin.ToUpper() });
            modelBuilder.Entity<ApplicationRole>().HasData(new ApplicationRole { Id = ClientRoleID, Name = Roles.Client, NormalizedName = Roles.Client.ToUpper() });

            var AdminUserID = 1;
            var ClientUserID = 2;
            var hasher = new PasswordHasher<ApplicationUser>();

            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = AdminUserID,
                FirstName = "Abbas",
                LastName = "Faour",
                DateOfBirth = new DateTime(2000, 05, 26),
                Email = "Abbasfaour25@gmail.com",
                EmailConfirmed = true,
                UserName = "abbasfaour",
                PasswordHash = hasher.HashPassword(null, "Pa$$w0rd"),
                PhoneNumber = "71435810",
                CreatedAt = DateTime.Now,
                CreatedBy = "Data-Seed",
                PhoneNumberConfirmed = true
            });
            
            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = ClientUserID,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(2000, 10, 10),
                Email = "johndoe@gmail.com",
                EmailConfirmed = true,
                UserName = "johndoe",
                PasswordHash = hasher.HashPassword(null, "Pa$$w0rd"),
                PhoneNumber = "7012345",
                CreatedAt = DateTime.Now,
                CreatedBy = "Data-Seed",
                PhoneNumberConfirmed = true
            });

            modelBuilder.Entity<ApplicationUserRole>().HasData(new ApplicationUserRole { UserId = AdminUserID, RoleId = AdminRoleID });
            modelBuilder.Entity<ApplicationUserRole>().HasData(new ApplicationUserRole { UserId = ClientUserID, RoleId = ClientRoleID });
        }
    }
}