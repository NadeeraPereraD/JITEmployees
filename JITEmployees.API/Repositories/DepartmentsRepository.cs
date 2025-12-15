using JITEmployees.API.Interfaces;
using JITEmployees.API.Models.Departments;
using Microsoft.Data.SqlClient;
using System.Data;


namespace JITEmployees.API.Repositories
{
    public class DepartmentsRepository : IDepartmentRepository
    {
        private readonly string _connectionString = null!;

        public DepartmentsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
        }

        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> CreateAsync(DepartmentsCreateDto dto)
        {
            try
            {
                await using var conn = new SqlConnection(_connectionString);
                await conn.OpenAsync();

                await using var cmd = new SqlCommand("usp_Departments_Create", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add(new SqlParameter("@DepartmentCode", SqlDbType.NVarChar)
                { Value = dto.DepartmentCode });

                cmd.Parameters.Add(new SqlParameter("@DepartmentName", SqlDbType.NVarChar)
                { Value = dto.DepartmentName });

                var errorParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 500)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(errorParam);

                var successParam = new SqlParameter("@SuccessMessage", SqlDbType.NVarChar, 500)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(successParam);

                await cmd.ExecuteNonQueryAsync();

                var errorMessage = errorParam.Value as string;
                var successMessage = successParam.Value as string;

                return (
                    IsSuccess: string.IsNullOrEmpty(errorMessage),
                    ErrorMessage: errorMessage,
                    SuccessMessage: successMessage
                );

            }
            catch
            {
                throw;
            }
        }
        public async Task<(IEnumerable<DepartmentsDto> departments, string? ErrorMessage, string? SuccessMessage)> GetAllAsync()
        {
            var departments = new List<DepartmentsDto>();

            try
            {
                await using var conn = new SqlConnection(_connectionString);
                await conn.OpenAsync();

                await using var cmd = new SqlCommand("usp_Departments_GetAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                var errorParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 500)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(errorParam);

                var successParam = new SqlParameter("@SuccessMessage", SqlDbType.NVarChar, 500)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(successParam);

                await using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    departments.Add(new DepartmentsDto
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        DepartmentCode = reader.GetString(reader.GetOrdinal("DepartmentCode")),
                        DepartmentName = reader.GetString(reader.GetOrdinal("DepartmentName"))
                    });
                }

                var errorMessage = errorParam.Value as string;
                var successMessage = successParam.Value as string;

                return (departments, errorMessage, successMessage);
            }
            catch
            {
                throw;
            }
        }
        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> UpdatedByKeyAsync(DepartmentsUpdateDto dto)
        {
            try
            {
                await using var conn = new SqlConnection(_connectionString);
                await conn.OpenAsync();

                await using var cmd = new SqlCommand("usp_Departments_Update", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = dto.Id });
                cmd.Parameters.Add(new SqlParameter("@DepartmentCode", SqlDbType.NVarChar) { Value = dto.DepartmentCode });
                cmd.Parameters.Add(new SqlParameter("@DepartmentName", SqlDbType.NVarChar) { Value = dto.DepartmentName });

                var errorParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 500)
                { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(errorParam);

                var successParam = new SqlParameter("@SuccessMessage", SqlDbType.NVarChar, 500)
                { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(successParam);

                await cmd.ExecuteNonQueryAsync();

                return (
                    IsSuccess: string.IsNullOrEmpty(errorParam.Value as string),
                    ErrorMessage: errorParam.Value as string,
                    SuccessMessage: successParam.Value as string
                );
            }
            catch
            {
                throw;
            }
        }
        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> DeleteByKeyAsync(DepartmentsDeleteDto dto)
        {
            try
            {
                await using var conn = new SqlConnection(_connectionString);
                await conn.OpenAsync();

                await using var cmd = new SqlCommand("usp_Departments_Delete", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = dto.Id });

                var errorParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 500)
                { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(errorParam);

                var successParam = new SqlParameter("@SuccessMessage", SqlDbType.NVarChar, 500)
                { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(successParam);

                await cmd.ExecuteNonQueryAsync();

                return (
                    IsSuccess: string.IsNullOrEmpty(errorParam.Value as string),
                    ErrorMessage: errorParam.Value as string,
                    SuccessMessage: successParam.Value as string
                );
            }
            catch
            {
                throw;
            }
        }

    }
}
