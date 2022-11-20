using ExercisesManager.API.DTOs;
using ExercisesManager.API.InputModels;
using ExercisesManager.Data.Entites;
using ExercisesManager.Data.Entites.Identity;

namespace ExercisesManager.API.Services.Interfaces
{
    public interface IExercisesService
    {
        Task<IEnumerable<ExerciseDTO>> GetExercisesAsync(string username, CancellationToken token);
        Task<ExerciseDTO> GetExerciseByIDAsync(long id, CancellationToken token);
        Task<ExerciseDTO> AddExerciseAsync(ExerciseInputModel exerciseInputModel, ApplicationUser user, CancellationToken token);
        Task DeleteExerciseAsync(Exercise exercise, CancellationToken token);
        Task<ExerciseDTO> UpdateExerciseAsync(Exercise exercise, ExerciseInputModel exerciseInputModel, CancellationToken token);
        Task<Exercise> FindExerciseAsync(long id, CancellationToken token);
    }
}