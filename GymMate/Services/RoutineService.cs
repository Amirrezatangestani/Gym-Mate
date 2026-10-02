using GymMate.Model;
using GymMate.Model.DTO;
using GymMate.Repository.IRepository;
using GymMate.Services.Interfaces;

namespace GymMate.Services.Implementations
{
    public class RoutineService : IRoutineService
    {
        private readonly IRoutineRepository _routineRepository;

        public RoutineService(IRoutineRepository routineRepository)
        {
            _routineRepository = routineRepository;
        }

        public async Task<Routine> AddRoutineAsync(
            string userId,
            AddRoutineDto model)
        {
            return await _routineRepository.AddRoutineAsync(userId, model);
        }

        public async Task<IEnumerable<ExerciseDto>> GetExercisesAsync()
        {
            return await _routineRepository.GetExercisesAsync();
        }

        public async Task<IEnumerable<Muscle>> GetAllMusclesAsync()
        {
            return await _routineRepository.GetAllMusclesAsync();
        }

        public async Task<bool> DeleteRoutineAsync(
            string userId,
            int routineId)
        {
            return await _routineRepository.DeleteRoutineAsync(
                userId,
                routineId);
        }

        public async Task<IEnumerable<RoutineDetailsDto>> GetUserRoutinesAsync(
            string userId)
        {
            return await _routineRepository.GetUserRoutinesAsync(userId);
        }

        public async Task<bool> UpdateRoutineNameAsync(
            string userId,
            UpdateRoutineNameDto dto)
        {
            return await _routineRepository.UpdateRoutineNameAsync(
                userId,
                dto);
        }

        public async Task<bool> AddExerciseToRoutineAsync(
            string userId,
            AddExerciseToRoutineDto dto)
        {
            return await _routineRepository.AddExerciseToRoutineAsync(
                userId,
                dto);
        }

        public async Task<bool> UpdateExerciseInRoutineAsync(
            string userId,
            UpdateExerciseInRoutineDto dto)
        {
            return await _routineRepository.UpdateExerciseInRoutineAsync(
                userId,
                dto);
        }

        public async Task<bool> RemoveExerciseFromRoutineAsync(
            string userId,
            RemoveExerciseFromRoutineDto dto)
        {
            return await _routineRepository.RemoveExerciseFromRoutineAsync(
                userId,
                dto);
        }

        public async Task<bool> AddExerciseAsync(AddExerciseDto model)
        {
            return await _routineRepository.AddExerciseAsync(model);
        }

        public async Task<bool> AddMuscleAsync(AddmuscelsDTO model)
        {
            return await _routineRepository.AddMuscleAsync(model);
        }
        public async Task<RoutineDetailsDto?> GetRoutineAsync(
    string userId,
    int routineId)
        {
            return await _routineRepository.GetRoutineAsync(userId, routineId);
        }
    }
}