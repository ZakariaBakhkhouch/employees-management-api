using EmployeesManagement.Application.Interfaces;
using EmployeesManagement.Domain.Entities;
using EmployeesManagement.Infrastructure.Data;

namespace EmployeesManagement.Infrastructure.Repositories
{
    public class EmployeesRepository : GenericRepository<Employee>, IEmployeesRepository
    {
        public EmployeesRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
