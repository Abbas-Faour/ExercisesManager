using System.ComponentModel.DataAnnotations;

namespace ExercisesManager.API.InputModels
{
    public class LoginUserInputModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}