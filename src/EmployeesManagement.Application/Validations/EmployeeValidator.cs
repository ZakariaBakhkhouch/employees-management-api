using EmployeesManagement.Application.DTOs;
using FluentValidation;

namespace EmployeesManagement.Application.Validations
{
    public class EmployeeValidator : AbstractValidator<CreateEmployeeDto>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.")
                .LessThan(DateTime.Now).WithMessage("Date of birth must be in the past.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.");

            RuleFor(x => x.HireDate)
                .NotEmpty().WithMessage("Hire date is required.");

            RuleFor(x => x.Salary)
                .NotEmpty().WithMessage("Salary is required.")
                .GreaterThan(0).WithMessage("Salary must be greater than zero.");

            RuleFor(x => x.Position)
                .NotEmpty().WithMessage("Position is required.")
                .MaximumLength(100).WithMessage("Position must not exceed 100 characters.");


        }
    }
}
