using EmployeesManagement.Application.Helpers;
using EmployeesManagement.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagement.Application.Commands.Employees.Handlers
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, BaseResponse>
    {

        private readonly IUnitOfWork _unitOfWork;

        public DeleteEmployeeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _unitOfWork.Employees.DeleteAsync(request.Id);
            }
            catch (Exception ex)
            {
                // Handle unexpected exceptions
                throw new ApplicationException("An unexpected error occurred while processing your request.", ex);
            }
        }
    }
}
