
namespace EmployeesManagement.Application.Mapping;

class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Employee, CreateEmployeeDto>()
            .ReverseMap();
    }
}
