using FluentValidation;
using JITEmployees.API.Models.Employees;

namespace JITEmployees.API.Services.Validations.Employees
{
    public class CreateEmployeeValidator : AbstractValidator<EmployeesCreateDto>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .Matches("^[A-Za-z]+$")
                .WithMessage("First name must contain letters only.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Matches("^[A-Za-z]+$")
                .WithMessage("Last name must contain letters only.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .Matches(@"^[^\s@]+@[^\s@]+\.[^\s@]+$")
                .WithMessage("Invalid email format.");

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Today)
                .WithMessage("Date of birth must be in the past.");

            RuleFor(x => x.Salary)
                .GreaterThan(0)
                .WithMessage("Salary must be greater than zero.");
        }
    }
}
