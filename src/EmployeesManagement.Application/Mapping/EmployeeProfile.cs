using AutoMapper;
using EmployeesManagement.Application.DTOs;
using EmployeesManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagement.Application.Mapping
{
    class EmployeeProfile : Profile
    {
        public EmployeeProfile() 
        {
            CreateMap<Employee, CreateEmployeeDto>()
                .ReverseMap();
        }
    }
}
