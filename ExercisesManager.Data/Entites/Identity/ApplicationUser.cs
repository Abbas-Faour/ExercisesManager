using Microsoft.AspNetCore.Identity;

namespace ExercisesManager.Data.Entites.Identity
{
    public class ApplicationUser : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; } = new HashSet<ApplicationUserRole>();
        public ICollection<UserExercises> UserExercises { get; set; } = new HashSet<UserExercises>();
    }
}