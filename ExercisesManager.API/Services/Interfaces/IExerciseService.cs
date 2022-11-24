using ExercisesManager.API.DTOs;
using ExercisesManager.API.InputModels;
using ExercisesManager.Data.Entities;
using ExercisesManager.Identity.Entities;

namespace ExercisesManager.API.Services.Interfaces
{
    public interface IExerciseService
    {
        Task<IEnumerable<ExerciseDTO>> GetExercisesAsync(string search, CancellationToken token);
        Task<ExerciseDTO> GetExerciseByIDAsync(long id,CancellationToken token);
        Task<ExerciseDTO> AddExerciseAsync(ExerciseInputModel exerciseInputModel, string username, CancellationToken token);
        Task DeleteExerciseAsync(Exercise exercise,CancellationToken token);
        Task<ExerciseDTO> UpdateExerciseAsync(Exercise exercise, string username, ExerciseInputModel exerciseInputModel, CancellationToken token);
        Task AssignExerciseToUser(long userID, long exerciseID, string username, CancellationToken token);
        Task<Exercise> FindExerciseAsync(long id, CancellationToken token);
        Task<IEnumerable<ExerciseDTO>> GetUserExercisesAsync(long userID, CancellationToken token);
    }
}