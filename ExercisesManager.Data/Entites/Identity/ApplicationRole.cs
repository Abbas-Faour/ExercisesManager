using ExercisesManager.Data.Entites.Identity;
using Microsoft.AspNetCore.Identity;

namespace ExercisesManager.Data.Entites
{
    public class ApplicationRole : IdentityRole<long>
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}