using EmployeesManagement.Application.Helpers;
using EmployeesManagement.Application.Interfaces;
using EmployeesManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagement.UnitTests
{
    public class EmployeesServiceFake : IEmployeesRepository
    {
        private readonly List<Employee> _employees;
        private readonly List<Department> departments;

        public EmployeesServiceFake()
        {
            departments = new List<Department>
            {
                new Department { Name = "Human Resources" },
                new Department { Name = "Finance" },
                new Department { Name = "Engineering" },
                new Department { Name = "Marketing" },
                new Department { Name = "Sales" }
            };

            _employees = new List<Employee>
            {
                new Employee {
                    FirstName = "Alice", LastName = "Johnson",
                    DateOfBirth = new DateTime(1990, 5, 12),
                    Email = "alice.johnson@example.com",
                    PhoneNumber = "555-1001",
                    HireDate = DateTime.Now.AddYears(-5),
                    Salary = 50000,
                    Position = "HR Specialist",
                    DepartmentId = departments.First(d => d.Name == "Human Resources").ID
                },
                new Employee {
                    FirstName = "Bob", LastName = "Smith",
                    DateOfBirth = new DateTime(1985, 11, 20),
                    Email = "bob.smith@example.com",
                    PhoneNumber = "555-1002",
                    HireDate = DateTime.Now.AddYears(-8),
                    Salary = 60000,
                    Position = "HR Manager",
                    DepartmentId = departments.First(d => d.Name == "Human Resources").ID
                },
                new Employee {
                    FirstName = "Clara", LastName = "Davis",
                    DateOfBirth = new DateTime(1992, 1, 8),
                    Email = "clara.davis@example.com",
                    PhoneNumber = "555-1003",
                    HireDate = DateTime.Now.AddYears(-2),
                    Salary = 45000,
                    Position = "Recruiter",
                    DepartmentId = departments.First(d => d.Name == "Human Resources").ID
                },
            };
        }

        // Get all with pagination
        public Task<BaseResponse> GetAllAsync(int pageNumber, int pageSize)
        {
            var skip = (pageNumber - 1) * pageSize;
            var employees = _employees.Skip(skip).Take(pageSize).ToList();

            return Task.FromResult(new BaseResponse
            {
                Success = true,
                Message = "Employees retrieved successfully",
                Data = employees
            });
        }

        // Get by Id
        public Task<BaseResponse> GetByIdAsync(Guid id)
        {
            var employee = _employees.FirstOrDefault(e => e.ID == id);
            if (employee == null)
            {
                return Task.FromResult(new BaseResponse
                {
                    Success = false,
                    Message = "Employee not found"
                });
            }

            return Task.FromResult(new BaseResponse
            {
                Success = true,
                Message = "Employee retrieved successfully",
                Data = employee
            });
        }

        // Add
        public Task<BaseResponse> AddAsync(Employee entity)
        {
            entity.ID = Guid.NewGuid();
            _employees.Add(entity);

            return Task.FromResult(new BaseResponse
            {
                Success = true,
                Message = "Employee added successfully",
                Data = entity
            });
        }

        // Update
        public Task<BaseResponse> UpdateAsync(Guid id, Employee entity)
        {
            var existing = _employees.FirstOrDefault(e => e.ID == entity.ID);
            if (existing == null) return null;

            existing.FirstName = entity.FirstName;
            existing.LastName = entity.LastName;
            existing.DateOfBirth = entity.DateOfBirth;
            existing.Email = entity.Email;
            existing.PhoneNumber = entity.PhoneNumber;
            existing.HireDate = entity.HireDate;
            existing.Salary = entity.Salary;
            existing.Position = entity.Position;
            existing.DepartmentId = entity.DepartmentId;

            return Task.FromResult(new BaseResponse
            {
                Success = true,
                Message = "Employee added successfully",
                Data = existing
            });
        }

        // Delete
        public Task<BaseResponse> DeleteAsync(Guid id)
        {
            var employee = _employees.FirstOrDefault(e => e.ID == id);
            if (employee == null)
            {
                return Task.FromResult(new BaseResponse
                {
                    Success = false,
                    Message = "Employee not found"
                });
            }

            _employees.Remove(employee);

            return Task.FromResult(new BaseResponse
            {
                Success = true,
                Message = "Employee deleted successfully"
            });
        }

        // Exists
        public Task<bool> ExistsAsync(int id)
        {
            // Assuming Employee.Id is Guid, not int
            // If you actually need int, you should adapt Employee.Id type
            var exists = _employees.Any(e => e.ID.GetHashCode() == id);
            return Task.FromResult(exists);
        }

        public Task<BaseResponse> DeleteAllRecords()
        {
            _employees.Clear();
            return Task.FromResult(new BaseResponse
            {
                Success = true,
                Message = "All employee records deleted successfully."
            });
        }

        public Task<BaseResponse> GetAllAsync(int pageNumber, int pageSize, params Expression<Func<Employee, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        //Task<BaseResponse> IGenericRepository<Employee>.UpdateAsync(Employee entity)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
