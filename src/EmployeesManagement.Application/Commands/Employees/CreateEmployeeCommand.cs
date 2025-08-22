
namespace EmployeesManagement.Application.Commands.Employees;

public record CreateEmployeeCommand(CreateEmployeeDto EmployeeDto) : IRequest<BaseResponse>;

