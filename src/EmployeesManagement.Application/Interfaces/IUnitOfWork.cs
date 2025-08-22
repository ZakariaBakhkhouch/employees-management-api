
namespace EmployeesManagement.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IEmployeesRepository Employees { get; }
    IDepartmentRepository Departments { get; }
}