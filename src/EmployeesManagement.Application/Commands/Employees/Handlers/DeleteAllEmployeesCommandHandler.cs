
namespace EmployeesManagement.Application.Commands.Employees.Handlers;

public class DeleteAllEmployeesCommandHandler : IRequestHandler<DeleteAllEmployeesCommand, BaseResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAllEmployeesCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse> Handle(DeleteAllEmployeesCommand request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Employees.DeleteAllRecords();
    }
}
