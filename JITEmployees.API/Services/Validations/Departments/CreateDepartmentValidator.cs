using FluentValidation;
using JITEmployees.API.Models.Departments;
using JITEmployees.API.Models.Employees;

namespace JITEmployees.API.Services.Validations.Departments
{
    public class CreateDepartmentValidator : AbstractValidator<DepartmentsCreateDto>
    {
        public CreateDepartmentValidator() 
        {
            RuleFor(x => x.DepartmentName)
                .NotEmpty().WithMessage("Department name is required.")
                .Matches("^[A-Za-z]+$")
                .WithMessage("Department name must contain letters only.");
        }
    }
}
