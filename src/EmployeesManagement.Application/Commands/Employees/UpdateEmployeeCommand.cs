using EmployeesManagement.Application.DTOs;
using EmployeesManagement.Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagement.Application.Commands.Employees;

public record UpdateEmployeeCommand(Guid id, CreateEmployeeDto EmployeeDto) : IRequest<BaseResponse>;
