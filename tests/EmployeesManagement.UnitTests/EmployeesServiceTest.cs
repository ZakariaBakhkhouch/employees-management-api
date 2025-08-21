using AutoMapper;
using EmployeesManagement.API.Controllers;
using EmployeesManagement.Application.DTOs;
using EmployeesManagement.Application.Helpers;
using EmployeesManagement.Application.Interfaces;
using EmployeesManagement.Application.Queries.Employees;
using EmployeesManagement.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeesManagement.UnitTests;

public class EmployeesServiceTest
{
    private readonly EmployeesController _employeesController;
    private readonly EmployeesServiceFake _employeesServiceFake;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<IValidator<CreateEmployeeDto>> _validatorMock;

    public EmployeesServiceTest()
    {
        _employeesServiceFake = new EmployeesServiceFake();

        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _unitOfWorkMock.Setup(u => u.Employees).Returns(_employeesServiceFake);

        _mapperMock = new Mock<IMapper>();
        _mediatorMock = new Mock<IMediator>();
        _validatorMock = new Mock<IValidator<CreateEmployeeDto>>();

        _employeesController = new EmployeesController(
            _unitOfWorkMock.Object,
            _mapperMock.Object,
            _mediatorMock.Object,
            _validatorMock.Object
        );
    }

    [Fact]
    public async Task GetAllEmployees_ReturnsOkResult()
    {
        // Arrange
        var query = new GetEmployeesListQuery
        {
            PageNumber = 1,
            PageSize = 10
        };

        var employeesList = (await _employeesServiceFake.GetAllAsync(query.PageNumber, query.PageSize)).Data as List<Employee>;

        BaseResponse baseResponse = new()
        {
            Data = employeesList,
            Success = true,
            Message = "Employees retrieved successfully"
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetEmployeesListQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(baseResponse);

        // Act
        var result = await _employeesController.GetAllEmployees(query);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);

        var response = okResult.Value as BaseResponse;

        Assert.True(response.Success);
        Assert.NotNull(response.Data);
        Assert.Equal("Employees retrieved successfully", response.Message);
    }



    [Fact]
    public async Task GetEmployee_ExistingId_ReturnsOkResult()
    {
        // Arrange
        var testEmployee = (await _employeesServiceFake.GetAllAsync(1, 10)).Data as List<Employee>;
        var existingEmployee = testEmployee.First();

        BaseResponse baseResponse = new()
        {
            Data = existingEmployee,
            Success = true,
            Message = "Employee retrieved successfully"
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetEmployeeByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(baseResponse);

        // Act
        var result = await _employeesController.GetEmployeeById(existingEmployee.ID);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);

        var response = okResult.Value as BaseResponse;
        var employee = response!.Data as Employee;

        Assert.NotNull(employee);
        Assert.Equal(existingEmployee.ID, employee.ID);
        Assert.True(response.Success);

    }

    [Fact]
    public async Task GetEmployee_NonExistingId_ReturnsNotFoundResult()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();

        BaseResponse baseResponse = new()
        {
            Data = null,
            Success = false,
            Message = "Employee not found"
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetEmployeeByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(baseResponse);

        // Act
        var result = await _employeesController.GetEmployeeById(nonExistingId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);

        var response = notFoundResult.Value as BaseResponse;

        Assert.False(response.Success);
        Assert.Null(response.Data);
        Assert.Equal("Employee not found", response.Message);
    }


}
