namespace ExercisesManager.Data.Entities
{
    public class Exercise : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public ICollection<UserExercise> UserExercises { get; set; } = new HashSet<UserExercise>();
    }
}