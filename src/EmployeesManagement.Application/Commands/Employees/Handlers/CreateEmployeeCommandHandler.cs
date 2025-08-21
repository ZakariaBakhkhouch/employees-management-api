using AutoMapper;
using EmployeesManagement.Application.Helpers;
using EmployeesManagement.Application.Interfaces;
using EmployeesManagement.Domain.Entities;
using EmployeesManagement.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagement.Application.Commands.Employees.Handlers;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, BaseResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var employee = new Employee();

            _mapper.Map(request.EmployeeDto, employee);

            employee.AddDomainEvent(new EmployeeCreatedEvent(employee));

            return await _unitOfWork.Employees.AddAsync(employee);
        }

        catch (DbUpdateException dbEx)
        {
            // Handle database update-related exceptions
            // e.g., constraint violations, concurrency issues
            throw new ApplicationException("An error occurred while adding the book to the database.", dbEx);
        }
        catch (ArgumentNullException argEx)
        {
            // Handle null argument exceptions
            throw new ArgumentException("A required parameter is missing or invalid.", argEx);
        }
        catch (Exception ex)
        {
            // Handle unexpected exceptions
            throw new ApplicationException("An unexpected error occurred while processing your request.", ex);
        }
    }
}
