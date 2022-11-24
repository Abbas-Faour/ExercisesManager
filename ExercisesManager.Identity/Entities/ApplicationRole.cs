using Microsoft.AspNetCore.Identity;

namespace ExercisesManager.Data.Entities
{
    public class ApplicationRole : IdentityRole<long>
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}