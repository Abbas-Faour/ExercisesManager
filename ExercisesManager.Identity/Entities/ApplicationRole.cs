using Microsoft.AspNetCore.Identity;

namespace ExercisesManager.Identity.Entities
{
    public class ApplicationRole : IdentityRole<long>
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}