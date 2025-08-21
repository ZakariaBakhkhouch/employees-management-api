using EmployeesManagement.Application.Interfaces;
using EmployeesManagement.Domain.Entities;
using EmployeesManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagement.Infrastructure.Repositories;

public class DepartmentsRepository : GenericRepository<Department>, IDepartmentRepository
{
    public DepartmentsRepository(ApplicationDbContext context) : base(context)
    {
    }
}