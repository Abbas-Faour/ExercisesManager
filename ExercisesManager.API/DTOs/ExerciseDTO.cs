namespace ExercisesManager.API.DTOs
{
    public class ExerciseDTO
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
    }
}