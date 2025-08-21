using EmployeesManagement.Domain.Entities;

namespace EmployeesManagement.Application.Interfaces;

public interface IEmployeesRepository : IGenericRepository<Employee>
{
    // Additional methods specific to employee service can be added here
}
