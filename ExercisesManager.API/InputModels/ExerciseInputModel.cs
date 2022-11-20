using System.ComponentModel.DataAnnotations;

namespace ExercisesManager.API.InputModels
{
    public class ExerciseInputModel
    {

        [Required,MaxLength(225)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public TimeSpan Duration { get; set; }
    }
}