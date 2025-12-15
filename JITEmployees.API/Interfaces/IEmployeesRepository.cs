using JITEmployees.API.Models.Departments;
using JITEmployees.API.Models.Employees;

namespace JITEmployees.API.Interfaces
{
    public interface IEmployeesRepository
    {
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> CreateAsync(EmployeesCreateDto dto);
        Task<(IEnumerable<EmployeesDto> employees, string? ErrorMessage, string? SuccessMessage)> GetAllAsync();
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> UpdatedByKeyAsync(EmployeesUpdateDto dto);
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> DeleteByKeyAsync(EmployeesDeleteDto dto);
    }
}
