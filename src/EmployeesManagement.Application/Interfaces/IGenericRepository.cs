using EmployeesManagement.Application.Helpers;

namespace EmployeesManagement.Application.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<BaseResponse> GetAllAsync(int pageNumber, int pageSize);
    Task<BaseResponse> GetByIdAsync(Guid id);
    Task<BaseResponse> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<BaseResponse> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(int id);

    // Additional methods can be added as needed
}