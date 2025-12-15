using JITEmployees.API.Models.Departments;

namespace JITEmployees.API.Interfaces
{
    public interface IDepartmentsService
    {
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> CreateAsync(DepartmentsCreateDto dto);
        Task<(IEnumerable<DepartmentsDto> departments, string? ErrorMessage, string? SuccessMessage)> GetAllDepartmentsAsync();
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> UpdateAsyncById(DepartmentsUpdateDto dto);
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> DeleteAsyncById(DepartmentsDeleteDto dto);
    }
}
