using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagement.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IEmployeesRepository Employees { get; }
    IDepartmentRepository Departments { get; }


}