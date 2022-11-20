using ExercisesManager.API.DTOs;
using ExercisesManager.API.InputModels;
using ExercisesManager.API.Mapping;
using ExercisesManager.API.Services.Interfaces;
using ExercisesManager.Data;
using ExercisesManager.Data.Entites;
using ExercisesManager.Data.Entites.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExercisesManager.API.Services
{
    public class ExercisesService : IExercisesService
    {
        private readonly ApplicationDbContext _context;
        public ExercisesService(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<ExerciseDTO> AddExerciseAsync(ExerciseInputModel exerciseInputModel,ApplicationUser user)
        {
            var exerciseToAdd = exerciseInputModel.ToEntity();
            var exerciseToReturn = await _context.Exercises.AddAsync(exerciseToAdd);

            user.UserExercises.Add(new UserExercises 
            {
                ApplicationUser = user,
                Exercise = exerciseToReturn.Entity,
                ApplicationUserId = user.Id,
                ExerciseId = exerciseToReturn.Entity.ID

            });
            _context.Update(user);
            await _context.SaveChangesAsync();
            return exerciseToReturn.Entity.ToDTO();
        }

        public async Task DeleteExerciseAsync(Exercise exercise)
        {
            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();
        }

        public async Task<ExerciseDTO> GetExerciseByIDAsync(long id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            return exercise?.ToDTO();
        }

        public async Task<Exercise> FindExerciseAsync(long id)
        {
            return await _context.Exercises.FindAsync(id);
        }

        public async Task<IEnumerable<ExerciseDTO>> GetExercisesAsync(string username)
        {
            
            if(String.IsNullOrWhiteSpace(username))
            {
                return await _context.Exercises.Select(x => x.ToDTO()).ToListAsync();
            }

            var exercices = _context.Exercises.
                Include(x => x.UserExercises).
                    ThenInclude(x => x.ApplicationUser).
                AsQueryable();

            return await exercices.Where(x => x.UserExercises.
                    Any(x => x.ApplicationUser.UserName.Equals(username))).
                    Select(x => x.ToDTO()).
                    ToListAsync();
 
        }

        public async Task<ExerciseDTO> UpdateExerciseAsync(Exercise exercise, ExerciseInputModel exerciseInputModel)
        {
            exercise.Name = exerciseInputModel.Name;
            exercise.Description = exerciseInputModel.Description;
            exercise.Duration = exerciseInputModel.Duration;
            _context.Update(exercise);
            await _context.SaveChangesAsync();
            return exercise.ToDTO();
        }
    }
}