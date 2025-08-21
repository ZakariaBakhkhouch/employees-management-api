using EmployeesManagement.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagement.Infrastructure.Data
{
    public class DBInitializer
    {
        public static void SeedDataAsync(IApplicationBuilder applicationBuilder)
        {
            using (var servicescop = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = servicescop.ServiceProvider.GetService<ApplicationDbContext>();

                if (context is not null)
                {
                    if (!context.Departments.Any())
                    {
                        var departments = new List<Department>
                        {
                            new Department { Name = "Human Resources" },
                            new Department { Name = "Finance" },
                            new Department { Name = "Engineering" },
                            new Department { Name = "Marketing" },
                            new Department { Name = "Sales" }
                        };

                        context.Departments.AddRange(departments);
                        context.SaveChanges();
                    }

                    if (!context.Employees.Any())
                    {
                        var departments = context.Departments.ToList();

                        var employees = new List<Employee>
                        {
                            // HR
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

                            // Finance
                            new Employee {
                                FirstName = "David", LastName = "Miller",
                                DateOfBirth = new DateTime(1987, 7, 15),
                                Email = "david.miller@example.com",
                                PhoneNumber = "555-2001",
                                HireDate = DateTime.Now.AddYears(-6),
                                Salary = 70000,
                                Position = "Accountant",
                                DepartmentId = departments.First(d => d.Name == "Finance").ID
                            },
                            new Employee {
                                FirstName = "Eva", LastName = "Wilson",
                                DateOfBirth = new DateTime(1991, 9, 23),
                                Email = "eva.wilson@example.com",
                                PhoneNumber = "555-2002",
                                HireDate = DateTime.Now.AddYears(-4),
                                Salary = 72000,
                                Position = "Financial Analyst",
                                DepartmentId = departments.First(d => d.Name == "Finance").ID
                            },
                            new Employee {
                                FirstName = "Frank", LastName = "Taylor",
                                DateOfBirth = new DateTime(1983, 12, 3),
                                Email = "frank.taylor@example.com",
                                PhoneNumber = "555-2003",
                                HireDate = DateTime.Now.AddYears(-10),
                                Salary = 90000,
                                Position = "Finance Manager",
                                DepartmentId = departments.First(d => d.Name == "Finance").ID
                            },

                            // Engineering
                            new Employee {
                                FirstName = "George", LastName = "Anderson",
                                DateOfBirth = new DateTime(1990, 2, 18),
                                Email = "george.anderson@example.com",
                                PhoneNumber = "555-3001",
                                HireDate = DateTime.Now.AddYears(-7),
                                Salary = 95000,
                                Position = "Software Engineer",
                                DepartmentId = departments.First(d => d.Name == "Engineering").ID
                            },
                            new Employee {
                                FirstName = "Hannah", LastName = "Thomas",
                                DateOfBirth = new DateTime(1993, 6, 9),
                                Email = "hannah.thomas@example.com",
                                PhoneNumber = "555-3002",
                                HireDate = DateTime.Now.AddYears(-3),
                                Salary = 87000,
                                Position = "QA Engineer",
                                DepartmentId = departments.First(d => d.Name == "Engineering").ID
                            },
                            new Employee {
                                FirstName = "Ian", LastName = "Moore",
                                DateOfBirth = new DateTime(1988, 4, 25),
                                Email = "ian.moore@example.com",
                                PhoneNumber = "555-3003",
                                HireDate = DateTime.Now.AddYears(-9),
                                Salary = 120000,
                                Position = "Tech Lead",
                                DepartmentId = departments.First(d => d.Name == "Engineering").ID
                            },

                            // Marketing
                            new Employee {
                                FirstName = "Julia", LastName = "Martin",
                                DateOfBirth = new DateTime(1994, 8, 17),
                                Email = "julia.martin@example.com",
                                PhoneNumber = "555-4001",
                                HireDate = DateTime.Now.AddYears(-2),
                                Salary = 55000,
                                Position = "Marketing Coordinator",
                                DepartmentId = departments.First(d => d.Name == "Marketing").ID
                            },
                            new Employee {
                                FirstName = "Kevin", LastName = "Lee",
                                DateOfBirth = new DateTime(1989, 3, 14),
                                Email = "kevin.lee@example.com",
                                PhoneNumber = "555-4002",
                                HireDate = DateTime.Now.AddYears(-6),
                                Salary = 65000,
                                Position = "Marketing Specialist",
                                DepartmentId = departments.First(d => d.Name == "Marketing").ID
                            },
                            new Employee {
                                FirstName = "Laura", LastName = "White",
                                DateOfBirth = new DateTime(1985, 5, 30),
                                Email = "laura.white@example.com",
                                PhoneNumber = "555-4003",
                                HireDate = DateTime.Now.AddYears(-10),
                                Salary = 80000,
                                Position = "Marketing Manager",
                                DepartmentId = departments.First(d => d.Name == "Marketing").ID
                            },

                            // Sales
                            new Employee {
                                FirstName = "Michael", LastName = "Hall",
                                DateOfBirth = new DateTime(1991, 10, 22),
                                Email = "michael.hall@example.com",
                                PhoneNumber = "555-5001",
                                HireDate = DateTime.Now.AddYears(-4),
                                Salary = 60000,
                                Position = "Sales Representative",
                                DepartmentId = departments.First(d => d.Name == "Sales").ID
                            },
                            new Employee {
                                FirstName = "Nina", LastName = "Young",
                                DateOfBirth = new DateTime(1992, 11, 11),
                                Email = "nina.young@example.com",
                                PhoneNumber = "555-5002",
                                HireDate = DateTime.Now.AddYears(-3),
                                Salary = 58000,
                                Position = "Sales Associate",
                                DepartmentId = departments.First(d => d.Name == "Sales").ID
                            },
                            new Employee {
                                FirstName = "Oscar", LastName = "King",
                                DateOfBirth = new DateTime(1986, 9, 5),
                                Email = "oscar.king@example.com",
                                PhoneNumber = "555-5003",
                                HireDate = DateTime.Now.AddYears(-12),
                                Salary = 90000,
                                Position = "Sales Manager",
                                DepartmentId = departments.First(d => d.Name == "Sales").ID
                            }

                        };

                        context.Employees.AddRange(employees);
                        context.SaveChanges();
                    }

                    
                }

            }
        }
    }
}
