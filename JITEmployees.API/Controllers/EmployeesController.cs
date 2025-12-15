using JITEmployees.API.Exceptions;
using JITEmployees.API.Interfaces;
using JITEmployees.API.Models.Departments;
using JITEmployees.API.Models.Employees;
using JITEmployees.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JITEmployees.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _employeesService;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(IEmployeesService employeesService, ILogger<EmployeesController> logger)
        {
            _employeesService = employeesService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeesCreateDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Create called with null DTO");
                return BadRequest(new { Message = "Invalid request body" });
            }

            try
            {
                var (IsSuccess, ErrorMessage, SuccessMessage) = await _employeesService.CreateAsync(dto);

                if (!IsSuccess)
                {
                    _logger.LogWarning("Failed to create employee for DTO {@DTO}. Error: {Error}", dto, ErrorMessage);
                    return BadRequest(new { Message = ErrorMessage });
                }

                _logger.LogInformation("Employee created successfully for DTO {@DTO}", dto);
                return Ok(new { Message = SuccessMessage });
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Resource not found for DTO {@DTO}", dto);
                return NotFound(new { Message = ex.Message });
            }
            catch (UnauthorizedAccessAppException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt for DTO {@DTO}", dto);
                return Unauthorized(new { Message = ex.Message });
            }
            catch (ForbiddenAccessException ex)
            {
                _logger.LogWarning(ex, "Forbidden access attempt for DTO {@DTO}", dto);
                return Forbid();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while creating employee for DTO {@DTO}", dto);
                return StatusCode(500, new { Message = "Internal server error" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var (Employees, ErrorMessage, SuccessMessage) = await _employeesService.GetAllEmployeesAsync();

                if (!string.IsNullOrEmpty(ErrorMessage))
                {
                    _logger.LogWarning("Failed to fetch employees. Error: {Error}", ErrorMessage);
                    return BadRequest(new { Message = ErrorMessage });
                }

                _logger.LogInformation("Fetched {Count} employees successfully.", Employees.Count());
                return Ok(Employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while fetching employees.");
                return StatusCode(500, new { Message = "Internal server error" });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EmployeesUpdateDto dto)
        {
            if (dto == null || dto.Id <= 0)
            {
                _logger.LogWarning("Update called with invalid DTO {@DTO}", dto);
                return BadRequest(new { Message = "Invalid request body or missing Id" });
            }

            try
            {
                var (IsSuccess, ErrorMessage, SuccessMessage) = await _employeesService.UpdateAsyncById(dto);

                if (!IsSuccess)
                {
                    _logger.LogWarning("Failed to update employee {@DTO}. Error: {Error}", dto, ErrorMessage);
                    return BadRequest(new { Message = ErrorMessage });
                }

                _logger.LogInformation("Employee updated successfully {@DTO}", dto);
                return Ok(new { Message = SuccessMessage });
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Employee not found for update {@DTO}", dto);
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while updating employee {@DTO}", dto);
                return StatusCode(500, new { Message = "Internal server error" });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] EmployeesDeleteDto dto)
        {
            if (dto == null || dto.Id <= 0)
            {
                _logger.LogWarning("Delete called with invalid DTO {@DTO}", dto);
                return BadRequest(new { Message = "Invalid request body or missing Id" });
            }

            try
            {
                var (IsSuccess, ErrorMessage, SuccessMessage) = await _employeesService.DeleteAsyncById(dto);

                if (!IsSuccess)
                {
                    _logger.LogWarning("Failed to delete employee {@DTO}. Error: {Error}", dto, ErrorMessage);
                    return BadRequest(new { Message = ErrorMessage });
                }

                _logger.LogInformation("Employee deleted (soft delete) successfully {@DTO}", dto);
                return Ok(new { Message = SuccessMessage });
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Employee not found for delete {@DTO}", dto);
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while deleting employee {@DTO}", dto);
                return StatusCode(500, new { Message = "Internal server error" });
            }
        }
    }
}
