
namespace EmployeesManagement.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<BaseResponse> GetAllAsync(int pageNumber, int pageSize)
    {
        var totalItems = await _context.Set<T>().CountAsync();

        var entities = await _context.Set<T>()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();

        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        BaseResponse response = new()
        {
            Message = "List of items.",
            Data = new PaginatedResponse<T>(entities, pageNumber, pageSize, totalItems)
        };

        return response;
    }

    public async Task<BaseResponse> GetByIdAsync(Guid id)
    {
        BaseResponse response = new();

        var entity = await _context.Set<T>().FindAsync(id);

        if (entity == null)
        {
            response.Success = false;
            response.Message = $"No item with id {id} found";
            return response;
        }

        response.Data = entity;
        response.Message = "Item retreived successfully.";

        return response;
    }

    public async Task<BaseResponse> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();

        BaseResponse response = new()
        {
            Success = true,
            Message = "Item added successfully.",
            Data = entity
        };

        return response;
    }

    public async Task<BaseResponse> UpdateAsync(Guid id, T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        BaseResponse response = new()
        {
            Success = true,
            Message = "Item updated successfully.",
            Data = entity
        };

        return response;
    }

    public async Task<BaseResponse> DeleteAsync(Guid id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

            return new BaseResponse
            {
                Success = true,
                Message = "Item deleted successfully."
            };
        }
        else
        {
            return new BaseResponse
            {
                Success = false,
                Message = $"No Item with id {id} found"
            };
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id) != null;
    }

    public async Task<BaseResponse> DeleteAllRecords()
    {
        var totalItems = await _context.Set<T>().ToListAsync();

        if (totalItems == null || !totalItems.Any())
        {
            return new BaseResponse
            {
                Success = false,
                Message = "No items found to delete."
            };
        }

        _context.Set<T>().RemoveRange(totalItems);
        await _context.SaveChangesAsync();

        return new BaseResponse
        {
            Success = true,
            Message = "All items deleted successfully."
        };
    }
}