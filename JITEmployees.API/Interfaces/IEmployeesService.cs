using JITEmployees.API.Models.Departments;
using JITEmployees.API.Models.Employees;

namespace JITEmployees.API.Interfaces
{
    public interface IEmployeesService
    {
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> CreateAsync(EmployeesCreateDto dto);
        Task<(IEnumerable<EmployeesDto> employees, string? ErrorMessage, string? SuccessMessage)> GetAllEmployeesAsync();
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> UpdateAsyncById(EmployeesUpdateDto dto);
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> DeleteAsyncById(EmployeesDeleteDto dto);
    }
}
