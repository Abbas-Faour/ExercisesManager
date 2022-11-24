namespace ExercisesManager.Data.Entities
{
    public class UserExercise : BaseEntity
    {
        public long ExerciseId { get; set; }
        public long ApplicationUserId { get; set; }
    }
}