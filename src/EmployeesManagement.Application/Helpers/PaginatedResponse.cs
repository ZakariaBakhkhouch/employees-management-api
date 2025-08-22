

namespace EmployeesManagement.Application.Helpers
{
    public class PaginatedResponse<T>
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public long TotalItems { get; }
        public int TotalPages { get; }
        public IList<T>? Data { get; }
        public PaginatedResponse(IList<T> data, int pageNumber, int pageSize, long totalItems)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItems = totalItems;
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            Data = data;
        }
    }
}
