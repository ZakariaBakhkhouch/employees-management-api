using EmployeesManagement.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EmployeesManagement.Application.Commands.Employees.EventHandlers
{
    public class EmployeeCreatedEventHandler : INotificationHandler<EmployeeCreatedEvent>
    {
        private readonly ILogger<EmployeeCreatedEventHandler> _logger;

        public EmployeeCreatedEventHandler(ILogger<EmployeeCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(EmployeeCreatedEvent notification, CancellationToken cancellationToken)
        {
            // Log the employee creation event
            _logger.LogInformation("EmployeesManagement Domain Event: {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}