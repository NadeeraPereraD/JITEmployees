using JITEmployees.API.Models.Departments;

namespace JITEmployees.API.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> CreateAsync(DepartmentsCreateDto dto);
        Task<(IEnumerable<DepartmentsDto>departments , string? ErrorMessage, string? SuccessMessage)> GetAllAsync();
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> UpdatedByKeyAsync(DepartmentsUpdateDto dto);
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> DeleteByKeyAsync(DepartmentsDeleteDto dto);
    }
}
