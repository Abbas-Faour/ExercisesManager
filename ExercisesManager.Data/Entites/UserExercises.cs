using ExercisesManager.Data.Entites.Identity;

namespace ExercisesManager.Data.Entites
{
    public class UserExercises
    {
        public long ApplicationUserId { get; set; }
        public long ExerciseId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Exercise Exercise { get; set; }
    }
}