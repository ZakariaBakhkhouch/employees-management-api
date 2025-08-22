
namespace EmployeesManagement.Application.Commands.Employees.Handlers;

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
            throw new ApplicationException("An unexpected error occurred while processing your request.", ex);
        }
    }
}