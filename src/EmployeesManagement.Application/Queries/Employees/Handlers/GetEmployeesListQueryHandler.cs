
namespace EmployeesManagement.Application.Queries.Employees.Handlers;

public class GetEmployeesListQueryHandler : IRequestHandler<GetEmployeesListQuery, BaseResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetEmployeesListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse> Handle(GetEmployeesListQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.Employees.GetAllAsync(request.PageNumber, request.PageSize, x => x.Department);
        }
        catch (DbUpdateException dbEx)
        {
            throw new ApplicationException("An error occurred while getting the list of employees from the database.", dbEx);
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
