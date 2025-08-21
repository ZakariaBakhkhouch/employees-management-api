using AutoMapper;
using EmployeesManagement.Application.Helpers;
using EmployeesManagement.Application.Interfaces;
using EmployeesManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagement.Application.Queries.Employees.Handlers
{
    public class GetEmployeesListQueryHandler : IRequestHandler<GetEmployeesListQuery, BaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEmployeesListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse> Handle(GetEmployeesListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _unitOfWork.Employees.GetAllAsync(request.PageNumber, request.PageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
