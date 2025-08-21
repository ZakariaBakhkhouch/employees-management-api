using EmployeesManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagement.Application.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        // Additional methods specific to employee service can be added here
    }
}
