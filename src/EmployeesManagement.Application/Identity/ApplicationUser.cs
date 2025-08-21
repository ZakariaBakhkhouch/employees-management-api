using Microsoft.AspNetCore.Identity;

namespace EmployeesManagement.Application.Identity
{
    /// <summary>
    /// Represents an application user in the identity system.
    /// Additional properties can be added to this class as needed.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
