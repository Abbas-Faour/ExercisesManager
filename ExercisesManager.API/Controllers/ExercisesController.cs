using ExercisesManager.API.Constants;
using ExercisesManager.API.DTOs;
using ExercisesManager.API.InputModels;
using ExercisesManager.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExercisesManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "ADMIN")]
    public class ExercisesController : ControllerBase
    {
        private readonly IExercisesService _exercisesService;
        private readonly IUsersService _userService;

        public ExercisesController(IExercisesService exercisesService,IUsersService usersService)
        {
            _exercisesService = exercisesService;
            _userService = usersService;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseDTO>>> GetAll([FromQuery]string username)
        {
            return Ok( await _exercisesService.GetExercisesAsync(username));
        }

        [HttpGet("{id}",Name = "GetExerciseByID")]
        public async Task<ActionResult<ExerciseDTO>> GetByIDAsync(long id)
        {
            var exercise = await _exercisesService.GetExerciseByIDAsync(id);

            if(exercise is null)
                return NotFound();

            return Ok(exercise);
        }

        [HttpPost]
        public async Task<ActionResult<ExerciseDTO>> CreateAsync([FromBody]ExerciseInputModel exerciseInputModel)
        {
            var username = User.Identity.Name;
            var user = await _userService.GetUserByName(username);

            var exercise = await _exercisesService.AddExerciseAsync(exerciseInputModel,user);
            return CreatedAtRoute("GetExerciseByID", new { Id = exercise.ID }, exercise);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var exercise = await _exercisesService.FindExerciseAsync(id);

            if(exercise is null)
                return NotFound();

            await _exercisesService.DeleteExerciseAsync(exercise);
            return NoContent();

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ExerciseDTO>> UpdateAsync(long id, ExerciseInputModel exerciseInputModel)
        {
            var exercise = await _exercisesService.FindExerciseAsync(id);

            if (exercise is null)
                return NotFound();

            var exerciseToReturn = await _exercisesService.UpdateExerciseAsync(exercise, exerciseInputModel);
            return Ok(exerciseToReturn);

        }


    }
}