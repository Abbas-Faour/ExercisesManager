using ExercisesManager.API.DTOs;
using ExercisesManager.API.InputModels;
using ExercisesManager.API.Mapping;
using ExercisesManager.API.Services.Interfaces;
using ExercisesManager.Data;
using ExercisesManager.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExercisesManager.API.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly ApplicationDbContext _context;
        public ExerciseService(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<ExerciseDTO> AddExerciseAsync(ExerciseInputModel exerciseInputModel, string username, CancellationToken token)
        {
            var exercise = exerciseInputModel.ToEntity(username);
            var exerciseToReturn = await _context.Exercises.AddAsync(exercise);
            await _context.SaveChangesAsync();
            return exerciseToReturn.Entity.ToDTO();
        }

        public async Task AssignExerciseToUser(long userID, long exerciseID, string username, CancellationToken token)
        {
            var exercise = await _context.Exercises.Include(x => x.UserExercises).SingleOrDefaultAsync(x => x.Id == exerciseID);
            exercise.UserExercises.Add(new UserExercise
            {
                CreatedAt = DateTime.Now,
                CreatedBy = username,
                ApplicationUserId = userID
            });
            await _context.SaveChangesAsync(token);
        }

        public async Task DeleteExerciseAsync(Exercise exercise, CancellationToken token)
        {
            exercise.IsDeleted = true;
            _context.Update(exercise);
            await _context.SaveChangesAsync(token);
        }

        public async Task<Exercise> FindExerciseAsync(long id, CancellationToken token)
        {
            return await _context.Exercises.FindAsync(new object[] {id}, token);
        }

        public async Task<ExerciseDTO> GetExerciseByIDAsync(long id,CancellationToken token)
        {
            var exercise = await _context.Exercises.FindAsync(new object[] { id }, token);
            return exercise.ToDTO();
        }

        public async Task<IEnumerable<ExerciseDTO>> GetExercisesAsync(string search, CancellationToken token)
        {
            if(String.IsNullOrWhiteSpace(search))
                return await _context.Exercises.Select(x => x.ToDTO()).ToListAsync(token);

            return await _context.Exercises.Where(x => x.CreatedBy.ToLower().Equals(search.ToLower()))
                .Select(x => x.ToDTO())
                .ToListAsync(token);
        }

        public async Task<ExerciseDTO> UpdateExerciseAsync(Exercise exercise, string username, ExerciseInputModel exerciseInputModel, CancellationToken token)
        {
            exercise.Name = exerciseInputModel.Name;
            exercise.Description = exerciseInputModel.Description;
            exercise.Duration = exerciseInputModel.Duration;
            exercise.UpdateAt = DateTime.Now;
            exercise.UpdateBy = username;
            _context.Update(exercise);
            await _context.SaveChangesAsync(token);
            return exercise.ToDTO();
        }

        public async Task<IEnumerable<ExerciseDTO>> GetUserExercisesAsync(long userID, CancellationToken token)
        {
            var exercises = _context.Exercises.Include(x => x.UserExercises).AsQueryable();
            return await exercises.Where(x => x.UserExercises.Any(x => x.ApplicationUserId == userID))
                .Select(x => x.ToDTO()).ToListAsync(token);
        }
    }
}