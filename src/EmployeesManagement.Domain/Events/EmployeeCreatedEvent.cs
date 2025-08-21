using EmployeesManagement.Domain.Common;
using EmployeesManagement.Domain.Entities;

namespace EmployeesManagement.Domain.Events
{
    public class EmployeeCreatedEvent : BaseEvent
    {
        public Employee Employee { get; set; }

        public EmployeeCreatedEvent(Employee employee) 
        {
            Employee = employee ?? throw new ArgumentNullException(nameof(employee), "Employee cannot be null");
        }
    }
}
