using JITEmployees.API.Interfaces;
using JITEmployees.API.Models.Employees;

namespace JITEmployees.API.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository _repo;

        public EmployeesService(IEmployeesRepository repo)
        {
            _repo = repo;
        }

        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> CreateAsync(EmployeesCreateDto dto)
        {
            return await _repo.CreateAsync(dto);
        }
        public Task<(IEnumerable<EmployeesDto> employees, string? ErrorMessage, string? SuccessMessage)> GetAllEmployeesAsync()
        {
            return _repo.GetAllAsync();
        }
        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> UpdateAsyncById(EmployeesUpdateDto dto)
        {
            return await _repo.UpdatedByKeyAsync(dto);
        }
        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> DeleteAsyncById(EmployeesDeleteDto dto)
        {
            return await _repo.DeleteByKeyAsync(dto);
        }
    }
}
