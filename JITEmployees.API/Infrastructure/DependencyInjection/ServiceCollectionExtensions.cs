using JITEmployees.API.Interfaces;
using JITEmployees.API.Repositories;
using JITEmployees.API.Services;

namespace JITEmployees.API.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection repositories)
        {
            repositories.AddScoped<IDepartmentRepository, DepartmentsRepository>();
            repositories.AddScoped<IEmployeesRepository, EmployeesRepository>();
            return repositories;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentsService, DepartmentsService>();
            services.AddScoped<IEmployeesService, EmployeesService>();
            return services;
        }
    }
}
