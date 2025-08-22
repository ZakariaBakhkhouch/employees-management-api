
namespace EmployeesManagement.Application.Queries.Employees;

public record GetEmployeesListQuery(int PageNumber = 1, int PageSize = 5) : IRequest<BaseResponse>;
