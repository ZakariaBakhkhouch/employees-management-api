
namespace EmployeesManagement.Application.Queries.Employees.Handlers;

public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, BaseResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetEmployeeByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.Employees.GetByIdAsync(request.id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}