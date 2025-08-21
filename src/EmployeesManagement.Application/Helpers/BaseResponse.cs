using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagement.Application.Helpers
{
    public class BaseResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public DateTime Request_Time { get; set; } = DateTime.UtcNow;
        public object Data { get; set; } = new object();
    }
}
