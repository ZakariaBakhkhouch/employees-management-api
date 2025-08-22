
namespace EmployeesManagement.Application.Queries.Departments.Handlers;

public class GetDepartmentsListQueryHandler : IRequestHandler<GetDepartmentsListQuery, BaseResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDepartmentsListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse> Handle(GetDepartmentsListQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.Departments.GetAllAsync(request.PageNumber, request.PageSize);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}