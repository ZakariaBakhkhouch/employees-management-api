using AutoMapper;
using EmployeesManagement.Application.Interfaces;
using EmployeesManagement.Application.Queries.Departments;
using EmployeesManagement.Application.Queries.Departments.Handlers;
using EmployeesManagement.Application.Queries.Employees;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesManagement.API.Controllers;

/// <summary>
/// Departments Controller
/// </summary>
public class DepartmentsController : BaseController
{
    /// <summary>
    /// Departments Controller Constructor
    /// </summary>
    /// <param name="unitOfWork"></param>
    /// <param name="mapper"></param>
    /// <param name="mediatR"></param>
    public DepartmentsController(IUnitOfWork unitOfWork, 
        IMapper mapper, 
        IMediator mediatR) : base(unitOfWork, mapper, mediatR)
    {
    }

    /// <summary>
    /// Get all departments.
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllDepartments([FromQuery] GetDepartmentsListQuery query)
    {
        try
        {
            var response = await _mediatR.Send(query);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
        }
    }
}
