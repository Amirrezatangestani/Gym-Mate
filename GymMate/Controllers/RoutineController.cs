using GymMate.Data;
using GymMate.Model;
using GymMate.Model.DTO;
using GymMate.Models.DTO;
using GymMate.Repository;
using GymMate.Repository.IRepository;
using GymMate.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymMate.Controllers
{
    [Route("[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class RoutineController : ControllerBase
    {
        //Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxZjUyNzAwMy1iYTBkLTQ2MzMtOWQwNi1mZDhkNzVkMmQ3NTUiLCJyb2xlIjoiVXNlciIsIm5iZiI6MTc5MDkyMjc4OSwiZXhwIjoxNzkxNTI3NTg5LCJpYXQiOjE3OTA5MjI3ODl9.gVXVGLz9mKMqyN8O7eneta367GDbPsqFalb7N4b3JjE
        private readonly IRoutineRepository _routineRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly IRoutineService _routineService;
      

        public RoutineController(IRoutineRepository routineRepository, UserManager<ApplicationUser> userManager,ApplicationDbContext db, IRoutineService routine)
        {
            _routineRepository = routineRepository;
            _userManager = userManager;
            _db = db;
            _routineService = routine;
        }
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddRoutine([FromBody] AddRoutineDto model)
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();
            if (_routineService == null)
                throw new Exception("_routineService is NULL");

            if (user == null)
                throw new Exception("user is NULL");

            if (model == null)
                throw new Exception("model is NULL");

            

            var routine = await _routineService.AddRoutineAsync(user.Id, model);
            return Ok(new { routine.Id, routine.Name });
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> GetExercises()
        {
            var exercises = await _routineService.GetExercisesAsync();
            return Ok(exercises);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllMuscles()
        {
            var muscles = await _routineService.GetAllMusclesAsync();
            return Ok(muscles);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> UpdateRoutineName([FromBody] UpdateRoutineNameDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var result = await _routineService.UpdateRoutineNameAsync(user.Id, dto);
            return result ? Ok("Routine name updated.") : NotFound();
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> AddExerciseToRoutine([FromBody] AddExerciseToRoutineDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var result = await _routineService.AddExerciseToRoutineAsync(user.Id, dto);
            return result ? Ok("Exercise added.") : BadRequest("Exercise already exists or routine not found.");
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> UpdateExerciseInRoutine([FromBody] UpdateExerciseInRoutineDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var result = await _routineService.UpdateExerciseInRoutineAsync(user.Id, dto);
            return result ? Ok("Exercise updated.") : NotFound();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> RemoveExerciseFromRoutine([FromBody] RemoveExerciseFromRoutineDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var result = await _routineService.RemoveExerciseFromRoutineAsync(user.Id, dto);
            return result ? Ok("Exercise removed.") : NotFound();
        }



        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> DeleteRoutine(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var result = await _routineService.DeleteRoutineAsync(user.Id, id);
            if (!result)
                return NotFound("Routine not found or access denied.");

            return NoContent(); 
        }


        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> GetMyRoutines()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var routines = await _routineService.GetUserRoutinesAsync(user.Id);
            return Ok(routines);
        }

        [HttpGet]
        [Authorize]
        [HttpGet("GetRoutine")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

       
        public async Task<ActionResult<RoutineDetailsDto>> GetRoutine(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Unauthorized();

            var routine = await _routineService.GetRoutineAsync(user.Id, id);

            if (routine == null)
                return NotFound("Routine not found");

            return Ok(routine);
        }


        [HttpPost("AddExercise")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddExercise([FromForm] AddExerciseDto model)
        {
            if (string.IsNullOrWhiteSpace(model.Name) || model.ExerImg == null)
                return BadRequest("Name and image are required.");

            var result = await _routineService.AddExerciseAsync(model);
            if (!result)
                return BadRequest("Muscle group not found.");

            return Ok("Exercise added successfully.");
        }

        [HttpPost("AddMuscle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddMuscle([FromForm] AddmuscelsDTO model)
        {
            var result = await _routineService.AddMuscleAsync(model);
            if (!result)
                return BadRequest("Failed to add muscle. It may already exist or input was invalid.");

            return Ok("Muscle added successfully.");
        }


        
    }
}
