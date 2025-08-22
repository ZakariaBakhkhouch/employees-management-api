
namespace EmployeesManagement.Infrastructure.Repositories;

public class DepartmentsRepository : GenericRepository<Department>, IDepartmentRepository
{
    public DepartmentsRepository(ApplicationDbContext context) : base(context)
    {
    }
}