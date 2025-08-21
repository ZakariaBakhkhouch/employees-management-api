using EmployeesManagement.Application.DTOs;
using EmployeesManagement.Application.Helpers;
using MediatR;

namespace EmployeesManagement.Application.Commands.Employees;

public record CreateEmployeeCommand(CreateEmployeeDto EmployeeDto) : IRequest<BaseResponse>;

