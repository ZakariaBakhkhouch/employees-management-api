using EmployeesManagement.Application.Helpers;
using MediatR;

namespace EmployeesManagement.Application.Queries.Employees;

public record GetEmployeeByIdQuery(Guid id) : IRequest<BaseResponse>;

