using JITEmployees.API.Interfaces;
using JITEmployees.API.Models.Departments;

namespace JITEmployees.API.Services
{
    public class DepartmentsService : IDepartmentsService
    {
        private readonly IDepartmentRepository _repo;

        public DepartmentsService(IDepartmentRepository repo)
        {
            _repo = repo;
        }

        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> CreateAsync(DepartmentsCreateDto dto)
        {
            return await _repo.CreateAsync(dto);
        }
        public Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> GetAllDepartmentsAsync(DepartmentsDto dto)
        {
            return _repo.GetAllAsync(dto);
        }
        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> UpdateAsyncById(DepartmentsUpdateDto dto)
        {
            return await _repo.UpdatedByKeyAsync(dto);
        }
        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> DeleteAsyncById(DepartmentsDeleteDto dto)
        {
            return await _repo.DeleteByKeyAsync(dto);
        }
    }
}
