using JITEmployees.API.Exceptions;
using JITEmployees.API.Interfaces;
using JITEmployees.API.Models.Departments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JITEmployees.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentsService _departmentsService;
        private readonly ILogger<DepartmentsController> _logger;

        public DepartmentsController(IDepartmentsService departmentsService, ILogger<DepartmentsController> logger)
        {
            _departmentsService = departmentsService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepartmentsCreateDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Create called with null DTO");
                return BadRequest(new { Message = "Invalid request body" });
            }

            try
            {
                var (IsSuccess, ErrorMessage, SuccessMessage) = await _departmentsService.CreateAsync(dto);

                if (!IsSuccess)
                {
                    _logger.LogWarning("Failed to create department for DTO {@DTO}. Error: {Error}", dto, ErrorMessage);
                    return BadRequest(new { Message = ErrorMessage });
                }

                _logger.LogInformation("Department created successfully for DTO {@DTO}", dto);
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
                _logger.LogError(ex, "Unexpected error while creating department for DTO {@DTO}", dto);
                return StatusCode(500, new { Message = "Internal server error" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var (Departments, ErrorMessage, SuccessMessage) = await _departmentsService.GetAllDepartmentsAsync();

                if (!string.IsNullOrEmpty(ErrorMessage))
                {
                    _logger.LogWarning("Failed to fetch departments. Error: {Error}", ErrorMessage);
                    return BadRequest(new { Message = ErrorMessage });
                }

                _logger.LogInformation("Fetched {Count} departments successfully.", Departments.Count());
                return Ok(Departments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while fetching departments.");
                return StatusCode(500, new { Message = "Internal server error" });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DepartmentsUpdateDto dto)
        {
            if (dto == null || dto.Id <= 0)
            {
                _logger.LogWarning("Update called with invalid DTO {@DTO}", dto);
                return BadRequest(new { Message = "Invalid request body or missing Id" });
            }

            try
            {
                var (IsSuccess, ErrorMessage, SuccessMessage) = await _departmentsService.UpdateAsyncById(dto);

                if (!IsSuccess)
                {
                    _logger.LogWarning("Failed to update department {@DTO}. Error: {Error}", dto, ErrorMessage);
                    return BadRequest(new { Message = ErrorMessage });
                }

                _logger.LogInformation("Department updated successfully {@DTO}", dto);
                return Ok(new { Message = SuccessMessage });
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Department not found for update {@DTO}", dto);
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while updating department {@DTO}", dto);
                return StatusCode(500, new { Message = "Internal server error" });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DepartmentsDeleteDto dto)
        {
            if (dto == null || dto.Id <= 0)
            {
                _logger.LogWarning("Delete called with invalid DTO {@DTO}", dto);
                return BadRequest(new { Message = "Invalid request body or missing Id" });
            }

            try
            {
                var (IsSuccess, ErrorMessage, SuccessMessage) = await _departmentsService.DeleteAsyncById(dto);

                if (!IsSuccess)
                {
                    _logger.LogWarning("Failed to delete department {@DTO}. Error: {Error}", dto, ErrorMessage);
                    return BadRequest(new { Message = ErrorMessage });
                }

                _logger.LogInformation("Department deleted (soft delete) successfully {@DTO}", dto);
                return Ok(new { Message = SuccessMessage });
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Department not found for delete {@DTO}", dto);
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while deleting user role {@DTO}", dto);
                return StatusCode(500, new { Message = "Internal server error" });
            }
        }
    }
}
