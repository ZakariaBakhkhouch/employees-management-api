
namespace EmployeesManagement.Domain.Entities;

public class Department : BaseEntity
{
    public Department()
    {
        ID = Guid.NewGuid();
    }

    public string Name { get; set; } = string.Empty;
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
