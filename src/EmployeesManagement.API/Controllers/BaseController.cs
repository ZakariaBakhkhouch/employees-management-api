using AutoMapper;
using EmployeesManagement.Application.DTOs;
using EmployeesManagement.Application.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly IMediator _mediatR;

        public BaseController(IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IMediator mediatR)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediatR = mediatR;;
        }
    }
}
