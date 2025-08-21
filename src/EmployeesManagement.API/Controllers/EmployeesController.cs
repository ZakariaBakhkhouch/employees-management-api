using AutoMapper;
using EmployeesManagement.API.Extensions;
using EmployeesManagement.Application.Commands.Employees;
using EmployeesManagement.Application.DTOs;
using EmployeesManagement.Application.Helpers;
using EmployeesManagement.Application.Interfaces;
using EmployeesManagement.Application.Queries.Employees;
using EmployeesManagement.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EmployeesManagement.API.Controllers
{
    [AllowAnonymous]
    public class EmployeesController : BaseController
    {
        protected readonly IValidator<CreateEmployeeDto> _employeeValidator;

        public EmployeesController(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediatR, IValidator<CreateEmployeeDto> employeeValidator)
            : base(unitOfWork, mapper, mediatR)
        {
            _employeeValidator = employeeValidator;
        }

        /// <summary>
        /// Get all employees.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees([FromQuery] GetEmployeesListQuery query)
        {
            try
            {
                var result = await _mediatR.Send(query);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Get a employee by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _mediatR.Send(new GetEmployeeByIdQuery(id));

                if (result.Data is null)
                {
                    return NotFound(result);
                }

                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Add a new employee.
        /// </summary>
        /// <param name="bookDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddNewBook([FromBody] CreateEmployeeDto employeeDto)
        {
            try
            {
                var employeeValidator = await _employeeValidator.ValidateAsync(employeeDto);

                if (!employeeValidator.IsValid)
                {
                    employeeValidator.AddToModelState(ModelState);
                    return BadRequest(ModelState);
                }

                var result = await _mediatR.Send(new CreateEmployeeCommand(employeeDto));

                var employee = result.Data as Employee;

                return CreatedAtAction(nameof(GetEmployeeById), new { id = employee!.ID }, result);       
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Delete an employee by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _mediatR.Send(new DeleteEmployeeCommand(id));

                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }
    }
}
