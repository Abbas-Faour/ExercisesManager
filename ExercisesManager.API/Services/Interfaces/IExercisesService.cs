using ExercisesManager.API.DTOs;
using ExercisesManager.API.InputModels;
using ExercisesManager.Data.Entites;
using ExercisesManager.Data.Entites.Identity;

namespace ExercisesManager.API.Services.Interfaces
{
    public interface IExercisesService
    {
        Task<IEnumerable<ExerciseDTO>> GetExercisesAsync(string username);
        Task<ExerciseDTO> GetExerciseByIDAsync(long id);
        Task<ExerciseDTO> AddExerciseAsync(ExerciseInputModel exerciseInputModel, ApplicationUser user);
        Task DeleteExerciseAsync(Exercise exercise);
        Task<ExerciseDTO> UpdateExerciseAsync(Exercise exercise, ExerciseInputModel exerciseInputModel);
        Task<Exercise> FindExerciseAsync(long id);
    }
}