using ExercisesManager.API.DTOs;
using ExercisesManager.API.InputModels;
using ExercisesManager.API.Services.Interfaces;
using ExercisesManager.Identity.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExercisesManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Admin)]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService _exercisesService;
        private readonly IUserService _usersService;

        public ExerciseController(
            IExerciseService exercisesService,
            IUserService usersService
        )
        {
            this._usersService = usersService;
            _exercisesService = exercisesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseDTO>>> Get([FromQuery]string search, CancellationToken token)
        {
            var exercises = await _exercisesService.GetExercisesAsync(search,token);
            return Ok(exercises);
        }

        [HttpGet("{id}",Name = "GetExerciseByID")]
        public async Task<ActionResult<ExerciseDTO>> GetById(long id, CancellationToken token)
        {
            var exercise = await _exercisesService.GetExerciseByIDAsync(id,token);

            if(exercise is null)
                return NotFound();
            return Ok(exercise);
        }

        [HttpPost]
        public async Task<ActionResult<ExerciseDTO>> Create([FromBody] ExerciseInputModel exerciseInputModel, CancellationToken token)
        {
            var username = User.Identity.Name;
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException($"User.Identity.Name", "Username is null");

            var exercise = await _exercisesService.AddExerciseAsync(exerciseInputModel, username, token);
            return CreatedAtRoute("GetExerciseByID", new { Id = exercise.ID }, exercise);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id, CancellationToken token)
        {
            var exercise = await _exercisesService.FindExerciseAsync(id, token);

            if (exercise is null)
                return NotFound();
            await _exercisesService.DeleteExerciseAsync(exercise, token);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ExerciseDTO>> Edit(long id, ExerciseInputModel exerciseInputModel, CancellationToken token)
        {
            var username = User?.Identity?.Name;
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException($"User.Identity.Name", "Username is null");

            var exercise = await _exercisesService.FindExerciseAsync(id, token);

            if (exercise is null)
                return NotFound();

            var exerciseToReturn = await _exercisesService.UpdateExerciseAsync(exercise, username, exerciseInputModel, token);
            return Ok(exerciseToReturn);
        }


        [HttpPost("Assign")]
        public async Task<IActionResult> AssignExerciseToUser([FromBody] AssignExerciseToUserInputModel assignExerciseToUserInputModel, CancellationToken token)
        {
            var username = User?.Identity?.Name;
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException($"User.Identity.Name", "Username is null");

            var exercise = await _exercisesService.FindExerciseAsync(assignExerciseToUserInputModel.ExerciseID, token);
            if (exercise is null)
                return NotFound();

            var user = await _usersService.GetUserByID(assignExerciseToUserInputModel.UserID);
            if (user is null)
                return NotFound();

            await _exercisesService.AssignExerciseToUser(user.Id, exercise.Id, username, token);
            return Ok();
        }

        [HttpGet("UserExercises/{id}")]
        public async Task<ActionResult<IEnumerable<ExerciseDTO>>> GetUserExercises(long id, CancellationToken token)
        {
            var username = User?.Identity?.Name;
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException($"User.Identity.Name", "Username is null");

            var user = _usersService.GetUserByID(id);
            if (user is null)
                return NotFound();

            var exercises = await _exercisesService.GetUserExercisesAsync(id, token);
            return Ok(exercises);
        }


    }
}