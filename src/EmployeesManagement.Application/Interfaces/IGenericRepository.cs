
namespace EmployeesManagement.Application.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<BaseResponse> GetAllAsync(int pageNumber, int pageSize);
    Task<BaseResponse> GetByIdAsync(Guid id);
    Task<BaseResponse> AddAsync(T entity);
    Task<BaseResponse> UpdateAsync(Guid id, T entity);
    Task<BaseResponse> DeleteAsync(Guid id);
    Task<BaseResponse> DeleteAllRecords();
    Task<bool> ExistsAsync(int id);

    // Additional methods can be added as needed
}