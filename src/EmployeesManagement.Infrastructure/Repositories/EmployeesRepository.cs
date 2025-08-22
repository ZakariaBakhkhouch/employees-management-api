
namespace EmployeesManagement.Infrastructure.Repositories;

public class EmployeesRepository : GenericRepository<Employee>, IEmployeesRepository
{
    public EmployeesRepository(ApplicationDbContext context) : base(context)
    {
    }
}