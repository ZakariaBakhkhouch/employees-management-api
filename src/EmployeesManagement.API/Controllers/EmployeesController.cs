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
using System.ComponentModel.DataAnnotations;

namespace EmployeesManagement.API.Controllers
{
    /// <summary>
    /// Employees controller to manage employee records.
    /// </summary>
    [AllowAnonymous]
    public class EmployeesController : BaseController
    {
        /// <summary>
        /// Employee validator to validate employee requests.
        /// </summary>
        protected readonly IValidator<CreateEmployeeDto> _employeeValidator;

        /// <summary>
        /// EmployeesController constructor.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        /// <param name="mediatR"></param>
        /// <param name="employeeValidator"></param>
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
        /// Get employee by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById([Required] Guid id)
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
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] CreateEmployeeDto employeeDto)
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Edit employee by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] CreateEmployeeDto employeeDto)
        {
            try
            {
                var employeeValidator = await _employeeValidator.ValidateAsync(employeeDto);
             
                if (!employeeValidator.IsValid)
                {
                    employeeValidator.AddToModelState(ModelState);
                    return BadRequest(ModelState);
                
                }
                
                var result = await _mediatR.Send(new UpdateEmployeeCommand(id, employeeDto));
                
                if (result.Data is null)
                {
                    return NotFound(new { Message = "Employee not found." });
                
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
        /// Delete employee by id.
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Delete all employees.
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteAllEmployees()
        {
            try
            {
                var response = await _mediatR.Send(new DeleteAllEmployeesCommand());    
                
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }
    }
}
