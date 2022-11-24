using ExercisesManager.API.DTOs;
using ExercisesManager.API.InputModels;
using ExercisesManager.Data.Entities;

namespace ExercisesManager.API.Mapping
{
    public static class ExerciseMapping
    {
        public static Exercise ToEntity(this ExerciseInputModel exerciseInputModel, string username)
        {
            return new Exercise
            {
                Name = exerciseInputModel.Name,
                Duration = exerciseInputModel.Duration,
                Description = exerciseInputModel.Description,
                CreatedAt = DateTime.Now,
                CreatedBy = username
            };
        }

        public static ExerciseDTO ToDTO(this Exercise exercise)
        {
            return new ExerciseDTO
            {
                ID = exercise.Id,
                Name = exercise.Name,
                Duration = exercise.Duration,
                Description = exercise.Description
            };
        }
    }
}