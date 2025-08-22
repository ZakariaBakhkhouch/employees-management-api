
namespace EmployeesManagement.Application.Queries.Departments;

public record GetDepartmentsListQuery(int PageNumber = 1, int PageSize = 5) : IRequest<BaseResponse>;
