using EmployeesManagement.Application.Interfaces;
using EmployeesManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagement.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IEmployeesRepository Employees { get; }
        public IDepartmentRepository Departments { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Employees = new EmployeesRepository(_context);
            Departments = new DepartmentsRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
