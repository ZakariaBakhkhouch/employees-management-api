
namespace EmployeesManagement.Application.Commands.Employees;

public record UpdateEmployeeCommand(Guid id, CreateEmployeeDto EmployeeDto) : IRequest<BaseResponse>;
