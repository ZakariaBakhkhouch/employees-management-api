
namespace EmployeesManagement.Domain.Entities;

public class Employee : BaseEntity
{
    public Employee()
    {
        ID = Guid.NewGuid();
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }
    public decimal Salary { get; set; }
    public string Position { get; set; } = string.Empty;
    public Guid DepartmentId { get; set; }
    public Department Department { get; set; } = null!; 
}