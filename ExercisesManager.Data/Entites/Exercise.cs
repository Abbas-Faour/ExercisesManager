using System.ComponentModel.DataAnnotations;

namespace ExercisesManager.Data.Entites
{
    public class Exercise
    {

        [Key]
        public long ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public ICollection<UserExercises> UserExercises { get; set; } = new HashSet<UserExercises>();
    }
}