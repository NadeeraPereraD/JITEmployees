using JITEmployees.API.Interfaces;
using JITEmployees.API.Models.Departments;
using JITEmployees.API.Models.Employees;
using Microsoft.Data.SqlClient;
using System.Data;

namespace JITEmployees.API.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly string _connectionString = null!;
        public EmployeesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
        }

        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> CreateAsync(EmployeesCreateDto dto)
        {
            await using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            await using var transaction = await conn.BeginTransactionAsync();

            try
            {
                int departmentId;

                await using (var deptCmd = new SqlCommand(
                    @"SELECT Id 
              FROM Departments 
              WHERE DepartmentName = @DepartmentName 
                AND IsActive = 1", conn, (SqlTransaction)transaction))
                {
                    deptCmd.Parameters.Add(
                        new SqlParameter("@DepartmentName", SqlDbType.NVarChar, 100)
                        { Value = dto.DepartmentName });

                    var result = await deptCmd.ExecuteScalarAsync();

                    if (result == null)
                    {
                        await transaction.RollbackAsync();
                        return (false, "Invalid or inactive department.", null);
                    }

                    departmentId = Convert.ToInt32(result);
                }

                await using var cmd = new SqlCommand("usp_Employees_Create", conn, (SqlTransaction)transaction)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 50) { Value = dto.FirstName });
                cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 50) { Value = dto.LastName });
                cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 100) { Value = dto.Email });
                cmd.Parameters.Add(new SqlParameter("@DateOfBirth", SqlDbType.Date) { Value = dto.DateOfBirth });
                cmd.Parameters.Add(new SqlParameter("@Salary", SqlDbType.Decimal)
                {
                    Precision = 12,
                    Scale = 2,
                    Value = dto.Salary
                });
                cmd.Parameters.Add(new SqlParameter("@DepartmentId", SqlDbType.Int) { Value = departmentId });

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
                await transaction.CommitAsync();

                return (
                    IsSuccess: string.IsNullOrEmpty(errorParam.Value as string),
                    ErrorMessage: errorParam.Value as string,
                    SuccessMessage: successParam.Value as string
                );
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<(IEnumerable<EmployeesDto> employees, string? ErrorMessage, string? SuccessMessage)> GetAllAsync()
        {
            var employees = new List<EmployeesDto>();

            try
            {
                await using var conn = new SqlConnection(_connectionString);
                await conn.OpenAsync();

                await using var cmd = new SqlCommand("usp_Employees_GetAll", conn)
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
                    employees.Add(new EmployeesDto
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                        LastName = reader.GetString(reader.GetOrdinal("LastName")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                        DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                        Age = reader.GetInt32(reader.GetOrdinal("Age")),
                        Salary = reader.GetDecimal(reader.GetOrdinal("Salary")),
                        DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                        DepartmentName = reader.GetString(reader.GetOrdinal("DepartmentName")),
                        IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                        CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                        UpdatedDate = reader.GetDateTime(reader.GetOrdinal("UpdatedDate"))
                    });
                }

                var errorMessage = errorParam.Value as string;
                var successMessage = successParam.Value as string;

                return (employees, errorMessage, successMessage);
            }
            catch
            {
                throw;
            }
        }
        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> UpdatedByKeyAsync(EmployeesUpdateDto dto)
        {
            await using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            await using var transaction = await conn.BeginTransactionAsync();

            try
            {
                int departmentId;

                await using (var deptCmd = new SqlCommand(
                    @"SELECT Id
              FROM Departments
              WHERE DepartmentName = @DepartmentName
                AND IsActive = 1", conn, (SqlTransaction)transaction))
                {
                    deptCmd.Parameters.Add(
                        new SqlParameter("@DepartmentName", SqlDbType.NVarChar, 100)
                        { Value = dto.DepartmentName });

                    var result = await deptCmd.ExecuteScalarAsync();

                    if (result == null)
                    {
                        await transaction.RollbackAsync();
                        return (false, "Invalid or inactive department.", null);
                    }

                    departmentId = Convert.ToInt32(result);
                }

                await using var cmd = new SqlCommand("usp_Employees_Update", conn, (SqlTransaction)transaction)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = dto.Id });
                cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 50) { Value = dto.FirstName });
                cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 50) { Value = dto.LastName });
                cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 100) { Value = dto.Email });
                cmd.Parameters.Add(new SqlParameter("@DateOfBirth", SqlDbType.Date) { Value = dto.DateOfBirth });
                cmd.Parameters.Add(new SqlParameter("@Salary", SqlDbType.Decimal)
                {
                    Precision = 12,
                    Scale = 2,
                    Value = dto.Salary
                });
                cmd.Parameters.Add(new SqlParameter("@DepartmentId", SqlDbType.Int)
                { Value = departmentId });

                var errorParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 500)
                { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(errorParam);

                var successParam = new SqlParameter("@SuccessMessage", SqlDbType.NVarChar, 500)
                { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(successParam);

                await cmd.ExecuteNonQueryAsync();
                await transaction.CommitAsync();

                return (
                    IsSuccess: string.IsNullOrEmpty(errorParam.Value as string),
                    ErrorMessage: errorParam.Value as string,
                    SuccessMessage: successParam.Value as string
                );
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> DeleteByKeyAsync(EmployeesDeleteDto dto)
        {
            try
            {
                await using var conn = new SqlConnection(_connectionString);
                await conn.OpenAsync();

                await using var cmd = new SqlCommand("usp_Employees_Delete", conn)
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
