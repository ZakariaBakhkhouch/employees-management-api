
namespace EmployeesManagement.Application.Commands.Employees;

public record DeleteEmployeeCommand(Guid Id) : IRequest<BaseResponse>;