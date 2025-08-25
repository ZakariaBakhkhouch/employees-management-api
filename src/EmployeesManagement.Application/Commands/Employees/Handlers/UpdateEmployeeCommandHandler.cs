
namespace EmployeesManagement.Application.Commands.Employees.Handlers;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, BaseResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(request.id);

            if (employee.Data == null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = $"Employee with id {request.id} not found."
                };
            }

            var _employee = employee.Data as Employee;

            _employee.FirstName = request.EmployeeDto.FirstName;
            _employee.LastName = request.EmployeeDto.LastName;
            _employee.Email = request.EmployeeDto.Email;
            _employee.PhoneNumber = request.EmployeeDto.PhoneNumber;
            _employee.DateOfBirth = request.EmployeeDto.DateOfBirth;
            _employee.HireDate = request.EmployeeDto.HireDate;
            _employee.Salary = request.EmployeeDto.Salary;
            _employee.Position = request.EmployeeDto.Position;
            _employee.DepartmentId = request.EmployeeDto.DepartmentId;

            return await _unitOfWork.Employees.UpdateAsync(request.id, _employee);
        }
        catch (DbUpdateException dbEx)
        {
            throw new ApplicationException("An error occurred while updating theemployee in the database.", dbEx);
        }
        catch (ArgumentNullException argEx)
        {
            throw new ArgumentException("A required parameter is missing or invalid.", argEx);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An unexpected error occurred while processing your request.", ex);
        }
    }
}